using System.Configuration;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaCoreConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("consumeGroupId", IsRequired = true)]
        public string ConsumeGroupId => (string)this["consumeGroupId"];

        [ConfigurationProperty("publishClientId", IsRequired = true)]
        public string PublishClientId => (string)this["publishClientId"];

        [ConfigurationProperty("bootstrapServers", IsRequired = true)]
        public string BootstrapServers => (string)this["bootstrapServers"];

        [ConfigurationProperty("sslEnabled", IsRequired = true)]
        public bool SslEnabled => (bool)this["sslEnabled"];

        [ConfigurationProperty("sslCaLocation", IsRequired = false)]
        public string SslCaLocation => (string)this["sslCaLocation"];

        [ConfigurationProperty("sslCertificateLocation", IsRequired = false)]
        public string SslCertificateLocation => (string)this["sslCertificateLocation"];

        [ConfigurationProperty("sslCertificatePrivateKeyLocation", IsRequired = false)]
        public string SslCertificatePrivateKeyLocation => (string)this["sslCertificatePrivateKeyLocation"];
    }
}