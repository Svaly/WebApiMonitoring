using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Logs;
using Framework.Patterns.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Messaging.Kafka.Publish
{
    public sealed class KafkaPublisher : IKafkaPublisher
    {
        private readonly IKafkaPublisherFactory _kafkaPublisherFactory;
        private readonly IKafkaLogger _kafkaLogger;
        private readonly IKafkaConfigurationProvider _kafkaConfigurationProvider;
        private KafkaConnectionConfigModel _connectionConfig;
        private int _topicPartitionNumber;

        public KafkaPublisher(IKafkaPublisherFactory kafkaPublisherFactory, IKafkaLogger kafkaLogger, IKafkaConfigurationProvider kafkaConfigurationProvider)
        {
            _topicPartitionNumber = 0;
            _kafkaPublisherFactory = kafkaPublisherFactory;
            _kafkaLogger = kafkaLogger;
            _kafkaConfigurationProvider = kafkaConfigurationProvider;
        }

        public async Task PublishAsync(string connectionName, KeyValuePair<string, string> message) => await PublishAsync(connectionName, new[] { message });

        public async Task PublishAsync(string connectionName, IEnumerable<KeyValuePair<string, string>> messages)
        {
            Guard.NotNull(() => connectionName, connectionName);
            Guard.NotNull(() => messages, messages);

            if (!ValidateConnectionConfig(connectionName))
            {
                return;
            }

            using (var producer = CreateProducer())
            {
                while (!await PublishMessagesAsync(messages, producer) && _topicPartitionNumber < _connectionConfig.PartitionCount)
                {
                    _topicPartitionNumber++;
                    _kafkaLogger.CommitLogs(KafkaLogType.Publish);
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }

            _kafkaLogger.CommitLogs(KafkaLogType.Publish);
        }

        private IProducer<string, string> CreateProducer()
        {
            return _kafkaPublisherFactory.CreateProducer(_connectionConfig, ProducerLogHandler, ProducerErrorHandler);
        }

        private async Task<bool> PublishMessagesAsync(IEnumerable<KeyValuePair<string, string>> messages, IProducer<string, string> producer)
        {
            var kafkaMessages = ConvertKeyValuePairsToKafkaMessages(messages);
            int failedMessages = 0;

            foreach (var message in kafkaMessages)
            {
                if (!await PublishMessageAsync(message, producer))
                {
                    failedMessages++;
                    _kafkaLogger.AppendFailedMessageToLog(new KeyValuePair<string, string>(message.Key, message.Value));
                }
            }

            return failedMessages == 0 || failedMessages != kafkaMessages.Count;
        }

        private async Task<bool> PublishMessageAsync(Message<string, string> message, IProducer<string, string> producer)
        {
            var retryCounter = 0;

            while (retryCounter < _connectionConfig.RetryCount)
            {
                try
                {
                    await PublishAsync(message, producer);
                    return true;
                }
                catch (Exception e)
                {
                    retryCounter++;
                    _kafkaLogger.AppendExceptionToLog(e);
                }
            }

            return false;
        }

        private async Task<DeliveryResult<string, string>> PublishAsync(Message<string, string> message, IProducer<string, string> producer)
        {
            if (_connectionConfig.SinglePartitionPublishPolicyEnabled)
            {
                var topicPartition = new TopicPartition(_connectionConfig.Topic, new Partition(_topicPartitionNumber));
                return await producer.ProduceAsync(topicPartition, message);
            }

            return await producer.ProduceAsync(_connectionConfig.Topic, message);
        }

        private void ProducerErrorHandler(IProducer<string, string> sender, Error error) => _kafkaLogger.AppendErrorToLog(error);

        private void ProducerLogHandler(IProducer<string, string> sender, LogMessage logMessage) => _kafkaLogger.AppendMessageToLog(logMessage);

        private bool ValidateConnectionConfig(string connectionName)
        {
            _connectionConfig = _kafkaConfigurationProvider.GetPublishConnectionConfiguration(connectionName) as KafkaConnectionConfigModel;
            return _connectionConfig != null && _connectionConfig.ConnectionIsEnabled;
        }

        private ICollection<Message<string, string>> ConvertKeyValuePairsToKafkaMessages(IEnumerable<KeyValuePair<string, string>> messages)
        {
            return messages.Select(m => new Message<string, string> { Key = m.Key, Value = m.Value }).ToList();
        }
    }
}