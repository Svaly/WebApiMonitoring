using System;
using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Consume
{
    public interface IKafkaConsumerFactory
    {
        IConsumer<string, string> CreateConsumer(
            KafkaConnectionConfigModel connectionConfig,
            Action<Consumer<string, string>, LogMessage> consumerLogHandler,
            Action<Consumer<string, string>, Error> consumerErrorHandler);
    }
}