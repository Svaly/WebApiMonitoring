using Framework.Patterns.Messaging;
using System.Collections.Generic;

namespace Framework.Patterns
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public AggregateRoot()
        {
            Events = new Queue<IEvent>();
        }

        protected void Enqueue(IEvent @event)
        {
            Events.Enqueue(@event);
        }

        public Queue<IEvent> Events { get; }
    }
}
