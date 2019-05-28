using System;
using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Publish
{
    public sealed class KafkaPublisherFactory : IKafkaPublisherFactory
    {
        public IProducer<string, string> CreateProducer(
            KafkaConnectionConfigModel connectionConfig,
            Action<IProducer<string, string>, LogMessage> producerLogHandler,
            Action<IProducer<string, string>, Error> producerErrorHandler)
        {
            var producerConfig = CreateProducerConfig(connectionConfig);
            ConfigureSsl(producerConfig, connectionConfig);
            ConfigureDebugging(producerConfig, connectionConfig);

            return CreateProducerBuilder(producerConfig, producerLogHandler, producerErrorHandler).Build();
        }

        private ProducerConfig CreateProducerConfig(KafkaConnectionConfigModel connectionConfig)
        {
            return new ProducerConfig
            {
                BootstrapServers = connectionConfig.BootstrapServers,
                ClientId = connectionConfig.PublishClientId,
                EnableIdempotence = true,
                Partitioner = Partitioner.Consistent,
                Acks = Acks.All
            };
        }

        private void ConfigureDebugging(ProducerConfig producerConfig, KafkaConnectionConfigModel connectionConfig)
        {
            if (connectionConfig.DebugMessagesEnabled) producerConfig.Debug = "all";
        }

        private void ConfigureSsl(ProducerConfig producerConfig, KafkaConnectionConfigModel connectionConfig)
        {
            if (connectionConfig.SslEnabled)
            {
                producerConfig.SecurityProtocol = SecurityProtocol.Ssl;
                producerConfig.SslCaLocation = connectionConfig.SslCaLocation;
                producerConfig.SslCertificateLocation = connectionConfig.SslCertificateLocation;
                producerConfig.SslKeyLocation = connectionConfig.SslCertificatePrivateKeyLocation;
            }
        }

        private ProducerBuilder<string, string> CreateProducerBuilder(
            ProducerConfig producerConfig,
            Action<IProducer<string, string>, LogMessage> producerLogHandler,
            Action<IProducer<string, string>, Error> producerErrorHandler)
        {
            var producerBuilder = new ProducerBuilder<string, string>(producerConfig);
            producerBuilder.SetLogHandler(producerLogHandler);
            producerBuilder.SetErrorHandler(producerErrorHandler);

            return producerBuilder;
        }
    }
}