using System;

namespace Framework.Patterns.Messaging
{
    public interface IEvent
    {
        Guid Id { get; }

        Guid AggregateId { get; }

        Guid? UserId { get; }

        long AggregateVersion { get; }

        long EventVersion { get; }

        Guid CorrelationId { get; }

        Guid CausationId { get; }

        string ApplicationName { get; }

        DateTime When { get; }
    }
}