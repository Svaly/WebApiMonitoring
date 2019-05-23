using Framework.Messaging.Configuration;
using Framework.Messaging.Kafka.Exceptions;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaConfigurationProvider : IKafkaConfigurationProvider
    {
        private readonly KafkaConnectionConfigurationSection _configuration;

        public KafkaConfigurationProvider()
        {
            _configuration = ConfigurationManager.GetSection("kafkaConnections") as KafkaConnectionConfigurationSection;

            if (_configuration is null)
            {
                throw new MissingKafkaConnectionConfigurationSectionException();
            }
        }

        public IConnectionConfigurationModel GetConsumeConnectionConfiguration(string connectionName)
        {
            return GetConnectionConfig(_configuration.ConsumeConnectionsCollection, connectionName).ToModel(GetCoreConfiguration());
        }

        public IConnectionConfigurationModel GetPublishConnectionConfiguration(string connectionName)
        {
            return GetConnectionConfig(_configuration.PublishConnectionsCollection, connectionName).ToModel(GetCoreConfiguration());
        }

        public IEnumerable<IConnectionConfigurationModel> GetAllEnabledConsumeConnectionsConfigurations()
        {
            return _configuration.ConsumeConnectionsCollection
                .Where(c => c.ConnectionEnabled)
                .Select(c => c.ToModel(GetCoreConfiguration()));
        }

        public IEnumerable<IConnectionConfigurationModel> GetAllEnabledPublishConnectionsConfigurations()
        {
            return _configuration.PublishConnectionsCollection
                .Where(c => c.ConnectionEnabled)
                .Select(c => c.ToModel(GetCoreConfiguration()));
        }

        private KafkaCoreConfigurationElement GetCoreConfiguration()
        {
            return _configuration.CoreConfiguration;
        }

        private KafkaConnectionConfigurationElement GetConnectionConfig(IEnumerable<KafkaConnectionConfigurationElement> elements, string connectionName)
        {
            var connectionConfig = elements.SingleOrDefault(c => c.ConnectionName == connectionName);

            if (connectionConfig == null)
            {
                throw new KafkaConnectionConfigNotFoundException(connectionName);
            }

            return connectionConfig;
        }
    }
}