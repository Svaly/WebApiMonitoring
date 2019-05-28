using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Extensions;
using Framework.Messaging.Kafka.Logs;
using Framework.Patterns.Loging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Messaging.Kafka.Consume
{
    public sealed class KafkaConsumer : IKafkaConsumer
    {
        private readonly IKafkaConsumerFactory _kafkaConsumerFactory;
        private readonly IKafkaLogger _kafkaLogger;
        private readonly ILogsProcessor _logsProcessor;

        public KafkaConsumer(IKafkaConsumerFactory kafkaConsumerFactory, IKafkaLogger kafkaLogger, ILogsProcessor logsProcessor)
        {
            _kafkaConsumerFactory = kafkaConsumerFactory;
            _kafkaLogger = kafkaLogger;
            _logsProcessor = logsProcessor;
        }

        public async Task ListenInfiniteLoopAsync(
            KafkaConnectionConfigModel connectionConfig,
            IKafkaConsumedMessageProcessor kafkaConsumedMessageProcessor)
        {
            using (var consumer = _kafkaConsumerFactory.CreateConsumer(connectionConfig, LogHandler, ErrorHandler))
            {
                consumer.Subscribe(connectionConfig.Topic);

                while (true)
                {
                    var consumeResult = consumer.Consume();

                    ProcessMessage(
                        kafkaConsumedMessageProcessor,
                        consumeResult.ToKeyValuePair(),
                        connectionConfig.ConnectionName);
                    CommitResultToBroker(consumer, consumeResult);

                    _kafkaLogger.CommitLogs(KafkaLogType.Consume);
                    await _logsProcessor.ProcessAsync();
                }
            }
        }

        private void ProcessMessage(
            IKafkaConsumedMessageProcessor kafkaConsumedMessageProcessor,
            KeyValuePair<string, string> message,
            string connectionName)
        {
                try
                {
                    kafkaConsumedMessageProcessor.ProcessMessage(message, connectionName);
                }
                catch (Exception e)
                {
                    _kafkaLogger.AppendExceptionToLog(e);
                    _kafkaLogger.AppendFailedMessageToLog(message);
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

        private void ErrorHandler(object sender, Error error)
        {
            _kafkaLogger.AppendErrorToLog(error);
        }

        private void LogHandler(object sender, LogMessage logMessage)
        {
            _kafkaLogger.AppendMessageToLog(logMessage);
        }
    }
}