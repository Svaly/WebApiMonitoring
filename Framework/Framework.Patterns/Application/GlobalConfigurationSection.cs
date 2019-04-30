using System.Configuration;

namespace Framework.Patterns.Application
{
    public sealed class GlobalConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("properties", IsRequired = true)]
        public GlobalConfigurationElement Configuration => (GlobalConfigurationElement)this["properties"];
    }
}