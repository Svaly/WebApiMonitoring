namespace Framework.Monitoring.Logs.Configuration
{
    public sealed class LogsConfigurationProvider : ILogsConfigurationProvider
    {
        public LogsConfigurationModel GetConfig()
        {
            return new LogsConfigurationModel();
        }
    }
}
