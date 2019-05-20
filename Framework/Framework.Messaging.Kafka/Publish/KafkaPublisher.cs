using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Extensions;
using Framework.Messaging.Kafka.Logs;
using Framework.Patterns.Validation;
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
                while (!await ProduceMessages(messages, producer) && _topicPartitionNumber < _connectionConfig.PartitionCount)
                {
                    _topicPartitionNumber++;
                    Debug.WriteLine(_topicPartitionNumber);
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }

        private IProducer<string, string> CreateProducer()
        {
            return _kafkaProducerFactory.CreateProducer(_connectionConfig, ProducerLogHandler, ProducerErrorHandler);
        }

        private async Task<bool> ProduceMessages(IEnumerable<KeyValuePair<string, string>> messages, IProducer<string, string> producer)
        {
            int failsCount = 0;

            foreach (var message in messages)
            {
                if (await ProduceMessage(message.ToKafkaMessage(), producer) is false)
                {
                    failsCount++;
                    _kafkaLogger.AppendFailedMessageToLog(message);
                }
            }

            _kafkaLogger.CommitLogs(KafkaLogType.Publish);
            return failsCount == 0 || failsCount != messages.Count();
        }

        private async Task<bool> ProduceMessage(Message<string, string> message, IProducer<string, string> producer)
        {
            var retryCounter = 0;

            while (retryCounter < _connectionConfig.RetryCount)
            {
                try
                {
                    Debug.WriteLine("witam");

                    await Send(message, producer);
                    Debug.WriteLine("witam 2");

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

        private bool ValidateConnectionConfig(string connectionName)
        {
            _connectionConfig = _kafkaConfigurationProvider.GetPublishConnectionConfiguration(connectionName) as KafkaConnectionConfigModel;
            return _connectionConfig != null && _connectionConfig.ConnectionIsEnabled;
        }
    }
}