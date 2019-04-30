using System.Configuration;

namespace Framework.Patterns.Application
{
    public sealed class GlobalConfigurationProvider : IGlobalConfigurationProvider
    {
        private readonly GlobalConfigurationSection _configSection;

        public GlobalConfigurationProvider()
        {
            _configSection = ConfigurationManager.GetSection("globalConfiguration") as GlobalConfigurationSection;
        }

        public GlobalConfigurationModel Configuration => _configSection.Configuration.ToModel();
    }
}