using System;
using Framework.Patterns;
using Framework.Patterns.Extension;

namespace Framework.Loging
{
    public sealed class DomainEventLog : Log
    {
        public DomainEventLog(IEvent @event) 
            : base(@event.CorrelationId, @event.CausationId, @event.GetType(), @event.ApplicationName, @event.ToJson())
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