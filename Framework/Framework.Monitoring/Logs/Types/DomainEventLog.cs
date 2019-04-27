using System;
using Framework.Patterns.Extension;
using Framework.Patterns.Messaging;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class DomainEventLog : Log
    {
        public DomainEventLog(IEvent @event) 
            : base(@event.CorrelationId, @event.CausationId, @event.GetType(), @event.ApplicationName, @event.ToJson(), LogType.DomainEvent)
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