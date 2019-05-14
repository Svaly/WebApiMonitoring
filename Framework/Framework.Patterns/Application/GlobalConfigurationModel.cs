namespace Framework.Patterns.Application
{
    public sealed class GlobalConfigurationModel
    {
        public GlobalConfigurationModel(string applicationName, string defaultPublishConnectionName)
        {
            ApplicationName = applicationName;
            DefaultPublishConnectionName = defaultPublishConnectionName;
        }

        public string ApplicationName { get; }

        public string DefaultPublishConnectionName { get; }
    }
}