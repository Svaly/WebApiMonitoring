using System.Configuration;

namespace Framework.Patterns.Application
{
    public sealed class GlobalConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("applicationName", IsRequired = true, IsKey = true)]
        public string ApplicationName => (string)this["applicationName"];

        public GlobalConfigurationModel ToModel()
        {
            return new GlobalConfigurationModel(ApplicationName);
        }
    }
}