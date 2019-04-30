using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Logs;
using System;
using System.Collections.Generic;
using System.Threading;
using Framework.Messaging.Kafka.Extensions;

namespace Framework.Messaging.Kafka.Consume
{
    public sealed class KafkaConsumer : IKafkaConsumer
    {
        private readonly IKafkaConsumerFactory _kafkaConsumerFactory;
        private readonly IKafkaLogger _kafkaLogger;

        public KafkaConsumer(IKafkaConsumerFactory kafkaConsumerFactory, IKafkaLogger kafkaLogger)
        {
            _kafkaConsumerFactory = kafkaConsumerFactory;
            _kafkaLogger = kafkaLogger;
        }

        public void ListenInfiniteLoop(KafkaConnectionConfigModel connectionConfig, IKafkaConsumerMessageHandler kafkaConsumerMessageHandler)
        {
            using (var consumer = _kafkaConsumerFactory.CreateConsumer(connectionConfig, ProducerLogHandler, ProducerErrorHandler))
            {
                consumer.Subscribe(connectionConfig.Topic);

                while (true)
                {
                    var consumeResult = consumer.Consume();

                    ProcessMessage(kafkaConsumerMessageHandler, consumeResult.ToKeyValuePair(), connectionConfig.RetryCount, connectionConfig.ConnectionName);
                    CommitResultToBroker(consumer, consumeResult);
                    _kafkaLogger.CommitLogs(KafkaLogType.Consume, $"Kafka consume failed after {connectionConfig.RetryCount} retries");
                }
            }
        }

        private void ProcessMessage(IKafkaConsumerMessageHandler kafkaConsumerMessageHandler, KeyValuePair<string, string> message, int retryCount, string connectionName)
        {
            var retryCounter = 0;

            while (retryCounter < retryCount)
            {
                try
                {
                    kafkaConsumerMessageHandler.HandleMessage(message, connectionName);
                    return;
                }
                catch (Exception e)
                {
                    retryCounter++;
                    _kafkaLogger.AppendExceptionToLog(e);
                    _kafkaLogger.AppendFailedMessageToLog(message);
                    Thread.Sleep(1000);
                }
            }
        }

        private void CommitResultToBroker(IConsumer<string, string> consumer, ConsumeResult<string, string> consumeResult)
        {
            try
            {
                consumer.Commit(consumeResult);
            }
            catch (Exception e)
            {
                _kafkaLogger.AppendExceptionToLog(e);
            }
        }

        private void ProducerErrorHandler(object sender, Error error) => _kafkaLogger.AppendErrorToLog(error);

        private void ProducerLogHandler(object sender, LogMessage logMessage) => _kafkaLogger.AppendMessageToLog(logMessage);
    }
}