using System.Collections.Generic;

namespace Framework.Patterns.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventQueue _eventQueue;

        public EventPublisher(IEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public void Send(IEvent @event)
        {
            _eventQueue.Enqueue(@event);
        }

        public void Send(Queue<IEvent> events)
        {
            while (events.Count > 0)
            {
                _eventQueue.Enqueue(events.Dequeue());
            }
        }

        public void Send(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                _eventQueue.Enqueue(@event);
            }
        }
    }
}