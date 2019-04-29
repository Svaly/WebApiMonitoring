using System;

namespace Framework.Monitoring.Logs.Types
{
    public interface ILog
    {
        Guid Id { get; }

        Guid CorrelationId { get; }

        Guid CausationId { get; }

        DateTime Timestamp { get; }

        string Type { get; }

        string ApplicationName { get; }

        string LogLevel { get; }

        string ProcessingScopeType { get; }
    }
}