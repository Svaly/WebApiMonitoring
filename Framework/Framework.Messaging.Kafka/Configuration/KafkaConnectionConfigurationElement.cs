using System.Configuration;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaConnectionConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionName", IsRequired = true, IsKey = true)]
        public string ConnectionName => (string) this["connectionName"];

        [ConfigurationProperty("connectionEnabled", IsRequired = true, IsKey = true)]
        public bool ConnectionEnabled => (bool) this["connectionEnabled"];

        [ConfigurationProperty("debugMessagesEnabled", IsRequired = true)]
        public bool DebugMessagesEnabled => (bool) this["debugMessagesEnabled"];

        [ConfigurationProperty("topic", IsRequired = true)]
        public string Topic => (string) this["topic"];

        [ConfigurationProperty("retryCount", IsRequired = true)]
        public int RetryCount => (int) this["retryCount"];

        [ConfigurationProperty("partitionCount", IsRequired = true)]
        public int PartitionCount => (int) this["partitionCount"];

        [ConfigurationProperty("singlePartitionPublishPolicyEnabled", IsRequired = false)]
        public bool? SinglePartitionPublishPolicyEnabled => (bool?) this["singlePartitionPublishPolicyEnabled"];

        public KafkaConnectionConfigModel ToModel(KafkaCoreConfigurationElement coreConfig)
        {
            return new KafkaConnectionConfigModel(
                ConnectionEnabled,
                ConnectionName,
                coreConfig.PublishClientId,
                coreConfig.ConsumeGroupId,
                coreConfig.BootstrapServers,
                Topic,
                coreConfig.SslEnabled,
                coreConfig.SslCaLocation,
                coreConfig.SslCertificateLocation,
                coreConfig.SslCertificatePrivateKeyLocation,
                DebugMessagesEnabled,
                RetryCount,
                SinglePartitionPublishPolicyEnabled,
                PartitionCount);
        }
    }
}