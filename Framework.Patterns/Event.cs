using System;

namespace Framework.Patterns
{
    public class Event : IEvent
    {
        public Event(Guid aggregateId, Guid? userId, long aggregateVersion, long eventVersion, Guid correlationId, Guid causationId, string applicationName)
        {
            AggregateId = aggregateId;
            UserId = userId;
            AggregateVersion = aggregateVersion;
            EventVersion = eventVersion;
            CorrelationId = correlationId;
            CausationId = causationId;
            ApplicationName = applicationName;
            When = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Guid AggregateId { get; }

        public Guid? UserId { get; }

        public long AggregateVersion { get; }

        public long EventVersion { get; }

        public Guid CorrelationId { get; }

        public Guid CausationId { get; }

        public string ApplicationName { get; }

        public DateTime When { get; }
    }
}
