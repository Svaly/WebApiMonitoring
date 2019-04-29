namespace Framework.Monitoring.Logs.Configuration
{
    public interface ILogsConfigurationProvider
    {
        LogsConfigurationModel GetConfig();
    }
}
