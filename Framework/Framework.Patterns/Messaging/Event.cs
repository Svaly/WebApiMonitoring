using System;

namespace Framework.Patterns.Messaging
{
    public class Event : IEvent
    {
        public Event(Guid aggregateId, Guid? causedById)
        {
            AggregateId = aggregateId;
            CausedById = causedById;
            When = DateTime.Now;
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; }

        public Guid AggregateId { get; }

        public Guid? CausedById { get; }

        public long AggregateVersion { get; set; }

        public long EventVersion { get; set; }

        public Guid CorrelationId { get; set; }

        public Guid CausationId { get; set; }

        public string ApplicationName { get; set; }

        public DateTime When { get; }
    }
}
