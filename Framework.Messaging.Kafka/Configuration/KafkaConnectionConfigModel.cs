using Framework.Messaging.Configuration;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaConnectionConfigModel : IConnectionConfigurationModel
    {
        public KafkaConnectionConfigModel(
            bool connectionIsEnabled,
            string connectionName,
            string publishClientId,
            string consumeGroupId,
            string bootstrapServers,
            string topic,
            bool sslEnabled,
            string sslCaLocation,
            string sslCertificateLocation,
            string sslCertificatePrivateKeyLocation,
            bool debugMessagesEnabled,
            int retryCount,
            bool? singlePartitionPublishPolicyEnabled,
            int partitionCount)
        {
            ConnectionIsEnabled = connectionIsEnabled;
            ConnectionName = connectionName;
            BootstrapServers = bootstrapServers;
            Topic = topic;
            SslEnabled = sslEnabled;
            SslCaLocation = sslCaLocation;
            SslCertificateLocation = sslCertificateLocation;
            SslCertificatePrivateKeyLocation = sslCertificatePrivateKeyLocation;
            DebugMessagesEnabled = debugMessagesEnabled;
            PublishClientId = publishClientId;
            ConsumeGroupId = consumeGroupId;
            RetryCount = retryCount;
            PartitionCount = partitionCount;
            SinglePartitionPublishPolicyEnabled = singlePartitionPublishPolicyEnabled ?? false;
        }

        public bool ConnectionIsEnabled { get; }

        public string ConnectionName { get; }

        public string PublishClientId { get; }

        public string ConsumeGroupId { get; }

        public string BootstrapServers { get; }

        public string Topic { get; }

        public bool SslEnabled { get; }

        public string SslCaLocation { get; }

        public string SslCertificateLocation { get; }

        public string SslCertificatePrivateKeyLocation { get; }

        public bool DebugMessagesEnabled { get; }

        public int RetryCount { get; }

        public bool SinglePartitionPublishPolicyEnabled { get; }

        public int PartitionCount { get; }
    }
}