namespace Framework.Patterns.Application
{
    public sealed class GlobalConfigurationModel
    {
        public GlobalConfigurationModel(string applicationName)
        {
            ApplicationName = applicationName;
        }

        public string ApplicationName { get; }
    }
}