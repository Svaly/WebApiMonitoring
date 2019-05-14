using System;

namespace Framework.Patterns.Messaging
{
    public interface IEvent
    {
        Guid EventId { get; }

        Guid AggregateId { get; }

        Guid? CausedById { get; }

        long AggregateVersion { get; }

        long EventVersion { get; }

        Guid CorrelationId { get; }

        Guid CausationId { get; }

        string ApplicationName { get; }

        DateTime When { get; }
    }
}