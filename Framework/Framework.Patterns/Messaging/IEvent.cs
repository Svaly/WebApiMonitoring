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

        Guid CorrelationId { get; set; }

        Guid UserId { get; }

        Guid CausationId { get; set; }

        string ApplicationName { get; set; }

        string ProcessingScope { get; set; }

        DateTime When { get; }
    }
}