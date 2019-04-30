using System;
using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Consume
{
    public interface IKafkaConsumerFactory
    {
        IConsumer<string, string> CreateConsumer(
            KafkaConnectionConfigModel connectionConfig,
            Action<IConsumer<string, string>, LogMessage> consumerLogHandler,
            Action<IConsumer<string, string>, Error> consumerErrorHandler);
    }
}