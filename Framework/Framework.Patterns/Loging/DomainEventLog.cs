using Framework.Patterns.Messaging;
using System;

namespace Framework.Patterns.Loging
{
    public sealed class DomainEventLog : Log
    {
        public DomainEventLog(IEvent @event, LogLevel logLevel)
            : base(logLevel)
        {
            AggregateId = @event.AggregateId;
            EventId = @event.EventId;
            UserId = @event.CausedById;
            AggregateVersion = @event.AggregateVersion;
            EventVersion = @event.EventVersion;
        }

        public Guid AggregateId { get; }

        public Guid EventId { get; }

        public Guid? UserId { get; }

        public long AggregateVersion { get; }

        public long EventVersion { get; }
    }
}