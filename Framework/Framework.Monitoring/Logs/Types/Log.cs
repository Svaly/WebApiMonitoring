using Framework.Monitoring.Logs.Factory;
using System;

namespace Framework.Monitoring.Logs.Types
{
    public class Log : ILog
    {
        public Log(LogLevel logLevel)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            LogLevel = logLevel.Value;
            Type = GetType().ToString();
            CorrelationId = ExecutionScope.Current.CorrelationId;
            CausationId = ExecutionScope.Current.CausationId;
            ProcessingScopeType = ExecutionScope.Current.ProcessingScopeType.Value;
            ApplicationName = ExecutionScope.ApplicationName;
        }

        public Guid Id { get; }

        public Guid CorrelationId { get; }

        public Guid CausationId { get; }

        public DateTime Timestamp { get; }

        public string Type { get; }

        public string ApplicationName { get; }

        public string LogLevel { get; }

        public string ProcessingScopeType { get; }
    }
}
