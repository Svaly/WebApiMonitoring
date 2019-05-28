using System.Collections.Generic;
using Framework.Patterns.Messaging;

namespace Framework.Patterns
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public AggregateRoot()
        {
            Events = new Queue<IEvent>();
        }

        public Queue<IEvent> Events { get; }

        protected void Enqueue(IEvent @event)
        {
            Events.Enqueue(@event);
        }
    }
}