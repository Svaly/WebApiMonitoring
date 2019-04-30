using System;
using Confluent.Kafka;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Consume
{
    public sealed class KafkaConsumerFactory : IKafkaConsumerFactory
    {
        public IConsumer<string, string> CreateConsumer(
          KafkaConnectionConfigModel connectionConfig,
          Action<IConsumer<string, string>, LogMessage> consumerLogHandler,
          Action<IConsumer<string, string>, Error> consumerErrorHandler)
        {
            var consumerConfig = CreateConsumerConfig(connectionConfig);
            ConfigureSsl(consumerConfig, connectionConfig);
            ConfigureDebugging(consumerConfig, connectionConfig);

            return CreateConsumerBuilder(consumerConfig, consumerLogHandler, consumerErrorHandler).Build();
        }

        private ConsumerConfig CreateConsumerConfig(KafkaConnectionConfigModel connectionConfig)
        {
            return new ConsumerConfig
            {
                GroupId = connectionConfig.ConsumeGroupId,
                BootstrapServers = connectionConfig.BootstrapServers,
                ClientId = connectionConfig.PublishClientId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
        }

        private void ConfigureDebugging(ConsumerConfig consumerConfig, KafkaConnectionConfigModel connectionConfig)
        {
            if (connectionConfig.DebugMessagesEnabled)
            {
                consumerConfig.Debug = "all";
            }
        }

        private void ConfigureSsl(ConsumerConfig consumerConfig, KafkaConnectionConfigModel connectionConfig)
        {
            if (connectionConfig.SslEnabled)
            {
                consumerConfig.SecurityProtocol = SecurityProtocol.Ssl;
                consumerConfig.SslCaLocation = connectionConfig.SslCaLocation;
                consumerConfig.SslCertificateLocation = connectionConfig.SslCertificateLocation;
                consumerConfig.SslKeyLocation = connectionConfig.SslCertificatePrivateKeyLocation;
            }
        }

        private ConsumerBuilder<string, string> CreateConsumerBuilder(
            ConsumerConfig consumerConfig,
            Action<IConsumer<string, string>, LogMessage> consumerLogHandler,
            Action<IConsumer<string, string>, Error> consumerErrorHandler)
        {
            var consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig);
            consumerBuilder.SetLogHandler(consumerLogHandler);
            consumerBuilder.SetErrorHandler(consumerErrorHandler);

            return consumerBuilder;
        }
    }
}