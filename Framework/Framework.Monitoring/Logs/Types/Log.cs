using System;

namespace Framework.Monitoring.Logs.Types
{
    public class Log : ILog
    {
        public Log(LogLevel logLevel)
        {
            LogId = Guid.NewGuid();
            Timestamp = DateTime.Now;
            LogLevel = logLevel.Value;
            LogType = GetType().ToString();
        }

        public Guid LogId { get; }

        public string LogType { get; }

        public DateTime Timestamp { get; }

        public string LogLevel { get; }

        public Guid CorrelationId { get; set; }

        public Guid CausationId { get; set; }

        public string ApplicationName { get; set; }

        public string ProcessingScope { get; set; }
    }
}
