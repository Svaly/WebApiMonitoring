using System.Collections.Generic;

namespace Framework.Messaging.Configuration
{
    public interface IConnectionConfigurationProvider
    {
        IConnectionConfigurationModel GetConsumeConnectionConfiguration(string connectionName);

        IConnectionConfigurationModel GetPublishConnectionConfiguration(string connectionName);

        IEnumerable<IConnectionConfigurationModel> GetAllEnabledConsumeConnectionsConfigurations();

        IEnumerable<IConnectionConfigurationModel> GetAllEnabledPublishConnectionsConfigurations();
    }
}