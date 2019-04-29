namespace Framework.Monitoring.Logs.Configuration
{
    public sealed class LogsConfigurationModel
    {
        public LogsConfigurationModel()
        {
            MessagingPublishConnectionName = "kafkaLogsConnection";
            EventLogName = "";
        }

        public string MessagingPublishConnectionName { get; }

        public string EventLogName { get; }
    }
}
