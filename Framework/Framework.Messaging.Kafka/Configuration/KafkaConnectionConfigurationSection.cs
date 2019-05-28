using System.Configuration;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaConnectionConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("consumeConnections", IsRequired = false)]
        public KafkaConnectionConfigurationCollection ConsumeConnectionsCollection =>
            (KafkaConnectionConfigurationCollection) this["consumeConnections"];

        [ConfigurationProperty("publishConnections", IsRequired = false)]
        public KafkaConnectionConfigurationCollection PublishConnectionsCollection =>
            (KafkaConnectionConfigurationCollection) this["publishConnections"];

        [ConfigurationProperty("coreConfiguration", IsRequired = true)]
        public KafkaCoreConfigurationElement CoreConfiguration => (KafkaCoreConfigurationElement) this["coreConfiguration"];
    }
}