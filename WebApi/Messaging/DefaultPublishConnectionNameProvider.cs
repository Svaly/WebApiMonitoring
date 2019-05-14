using Framework.Messaging.Publish;
using Framework.Patterns.Application;

namespace WebApi.Messaging
{
    public class DefaultPublishConnectionNameProvider : IDefaultPublishConnectionNameProvider
    {
        private readonly IGlobalConfigurationProvider _globalConfigurationProvider;

        public DefaultPublishConnectionNameProvider(IGlobalConfigurationProvider globalConfigurationProvider)
        {
            _globalConfigurationProvider = globalConfigurationProvider;
        }

        public string GetDefaultPublishConnectionName()
        {
            return _globalConfigurationProvider.Configuration.DefaultPublishConnectionName;
        }
    }
}