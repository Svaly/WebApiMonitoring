using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Loging;

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
                    var message = consumer.Consume();

                    ProcessMessage(kafkaConsumerMessageHandler, new ConsumeResult(message), connectionConfig.RetryCount, connectionConfig.ConnectionName);
                    CommitResultToBroker(consumer, message);
                }
            }
        }

        private void ProcessMessage(IKafkaConsumerMessageHandler kafkaConsumerMessageHandler, ConsumeResult consumeResult, int retryCount, string connectionName)
        {
            var retryCounter = 0;

            Console.WriteLine("Partition : " + consumeResult.TopicPartition);

            while (retryCounter < retryCount)
            {
                try
                {
                    kafkaConsumerMessageHandler.HandleMessage(new KeyValuePair<string, string>(consumeResult.Key, consumeResult.Value), connectionName);
                    return;
                }
                catch (Exception e)
                {
                    retryCounter++;
                    _kafkaLogger.LogException(e, consumeResult.Key, $"Kafka processing consumed message failed due to exception. Current retry number: {retryCounter}");
                    Thread.Sleep(1000);
                }
            }

            _kafkaLogger.LogWarning(consumeResult.Key, $"Kafka consume failed after {retryCounter} attempts. Message was skipped.");
        }

        private void CommitResultToBroker(IConsumer<string, string> consumer, ConsumeResult<string, string> consumeResult)
        {
            _kafkaLogger.CommitLogs();

            try
            {
                consumer.Commit(consumeResult);
            }
            catch (Exception e)
            {
                _kafkaLogger.LogWarning(e, consumeResult.Key, "Kafka commit result to broker failed");
            }
        }

        private void ProducerErrorHandler(object sender, Error error) => _kafkaLogger.AppendErrorToLog(error);

        private void ProducerLogHandler(object sender, LogMessage logMessage) => _kafkaLogger.AppendMessageToLog(logMessage);
    }
}