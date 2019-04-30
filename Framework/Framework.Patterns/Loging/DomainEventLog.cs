using System;
using Framework.Patterns.Messaging;

namespace Framework.Patterns.Loging
{
    public sealed class DomainEventLog : Log
    {
        public DomainEventLog(IEvent @event, LogLevel logLevel)
            : base(logLevel)
        {
            AggregateId = @event.AggregateId;
            EventId = @event.Id;
            UserId = @event.UserId;
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