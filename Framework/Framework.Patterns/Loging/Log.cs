using System;

namespace Framework.Patterns.Loging
{
    public class Log : ILog
    {
        public Log(LogLevel logLevel, string message = null)
        {
            Message = message;
            LogId = Guid.NewGuid();
            Timestamp = DateTime.Now;
            LogLevel = logLevel.Value;
            LogType = GetType().ToString();
        }

        public string Message { get; }

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