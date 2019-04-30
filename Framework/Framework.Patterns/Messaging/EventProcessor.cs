using System;

namespace Framework.Patterns.Messaging
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IEventQueue _eventQueue;

        public EventProcessor(IEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public void Process()
        {
            while (_eventQueue.HasEvents)
            {
                var @event = _eventQueue.Dequeue();

               // Console.WriteLine($"{@event}  {@event.EventId}  {@event.Number}");
            }
        }
    }
}