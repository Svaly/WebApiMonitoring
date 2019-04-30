using System;
using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Publish
{
    public interface IKafkaProducerFactory
    {
        IProducer<string, string> CreateProducer(
            KafkaConnectionConfigModel connectionConfig,
            Action<IProducer<string, string>, LogMessage> producerLogHandler,
            Action<IProducer<string, string>, Error> producerErrorHandler);
    }
}