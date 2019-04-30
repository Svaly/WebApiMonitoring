using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Loging;
using Framework.Patterns.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Messaging.Kafka.Publish
{
    public sealed class KafkaPublisher : IKafkaPublisher
    {
        private readonly IKafkaProducerFactory _kafkaProducerFactory;
        private readonly IKafkaLogger _kafkaLogger;
        private readonly IKafkaConfigurationProvider _kafkaConfigurationProvider;
        private KafkaConnectionConfigModel _connectionConfig;
        private int _topicPartitionNumber;

        public KafkaPublisher(IKafkaProducerFactory kafkaProducerFactory, IKafkaLogger kafkaLogger, IKafkaConfigurationProvider kafkaConfigurationProvider)
        {
            _topicPartitionNumber = 0;
            _kafkaProducerFactory = kafkaProducerFactory;
            _kafkaLogger = kafkaLogger;
            _kafkaConfigurationProvider = kafkaConfigurationProvider;
        }

        public async Task PublishAsync(string connectionName, KeyValuePair<string, string> message)
        {
            Guard.NotNull(() => connectionName, connectionName);
            Guard.NotNull(() => message, message);

            if (!ValidateConnectionConfig(connectionName))
            {
                return;
            }

            using (var producer = CreateProducer())
            {
                while (!await ProduceMessages(new[] { message }, producer))
                {
                    ChangePartitionNumber();
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }

            _kafkaLogger.CommitLogs();
        }

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
                while (!await ProduceMessages(messages, producer))
                {
                    ChangePartitionNumber();
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }

            _kafkaLogger.CommitLogs();
        }

        private IProducer<string, string> CreateProducer()
        {
            return _kafkaProducerFactory.CreateProducer(_connectionConfig, ProducerLogHandler, ProducerErrorHandler);
        }

        private async Task<bool> ProduceMessages(IEnumerable<KeyValuePair<string, string>> messages, IProducer<string, string> producer)
        {
            var kafkaMessages = ConvertKeyValuePairsToKafkaMessages(messages);
            var failedMessages = new List<Message<string, string>>();

            foreach (var message in kafkaMessages)
            {
                if (!await ProduceMessage(message, producer))
                {
                    failedMessages.Add(message);
                }
            }

            if (failedMessages.Count == 0 || failedMessages.Count != kafkaMessages.Count)
            {
                LogProduceFailed(failedMessages);
                return true;
            }

            return false;
        }

        private async Task<bool> ProduceMessage(Message<string, string> message, IProducer<string, string> producer)
        {
            var retryCounter = 0;

            while (retryCounter < _connectionConfig.RetryCount)
            {
                try
                {
                    await Send(message, producer);
                    return true;
                }
                catch (Exception ex)
                {
                    retryCounter++;
                    LogProduceException(ex, message.Key, retryCounter);
                }
            }

            return false;
        }

        private async Task Send(Message<string, string> message, IProducer<string, string> producer)
        {
            if (_connectionConfig.SinglePartitionPublishPolicyEnabled)
            {
                var topicPartition = new TopicPartition(_connectionConfig.Topic, new Partition(_topicPartitionNumber));
                 await producer.ProduceAsync(topicPartition, message);
            }

             await producer.ProduceAsync(_connectionConfig.Topic, message);
        }

        private void ProducerErrorHandler(IProducer<string, string> sender, Error error) => _kafkaLogger.AppendErrorToLog(error);

        private void ProducerLogHandler(IProducer<string, string> sender, LogMessage logMessage) => _kafkaLogger.AppendMessageToLog(logMessage);

        private void LogProduceFailed(IEnumerable<Message<string, string>> failedMessages)
        {
            foreach (var message in failedMessages)
            {
                _kafkaLogger.LogWarning(message.Key, $"Kafka publish failed after {_connectionConfig.RetryCount} attempts. Message was skipped.");
            }
        }

        private void LogProduceException(Exception exception, string messageKey, int retryCounter)
        {
            _kafkaLogger.LogException(exception, messageKey, $"Kafka publish failed due to unexpected exception. Retry counter {retryCounter}.");
        }

        private bool ValidateConnectionConfig(string connectionName)
        {
            _connectionConfig = _kafkaConfigurationProvider.GetPublishConnectionConfiguration(connectionName) as KafkaConnectionConfigModel;
            return _connectionConfig != null && _connectionConfig.ConnectionIsEnabled;
        }

        private void ChangePartitionNumber()
        {
            Random rnd = new Random();
            _topicPartitionNumber = rnd.Next(0, _connectionConfig.PartitionCount);
        }

        private ICollection<Message<string, string>> ConvertKeyValuePairsToKafkaMessages(IEnumerable<KeyValuePair<string, string>> messages)
        {
            return messages.Select(
                m => new Message<string, string>
                {
                    Key = m.Key,
                    Value = m.Value
                }).ToList();
        }
    }
}