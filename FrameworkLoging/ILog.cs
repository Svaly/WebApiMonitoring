using System;

namespace Framework.Loging
{
    public interface ILog
    {
        Guid Id { get; }

        Guid CorrelationId { get; }

        Guid CausationId { get; }

        DateTime Timestamp { get; }

        string Type { get; }

        string ApplicationName { get; }

        string Data { get; }
    }
}