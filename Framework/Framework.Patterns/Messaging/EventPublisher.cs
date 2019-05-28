using System.Collections.Generic;
using Framework.Patterns.Loging;

namespace Framework.Patterns.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventQueue _eventQueue;
        private readonly IExecutionScope _executionScope;

        public EventPublisher(IEventQueue eventQueue, IExecutionScope executionScope)
        {
            _eventQueue = eventQueue;
            _executionScope = executionScope;
        }

        public void Send(IEvent @event)
        {
            EnrichEvent(@event);
            _eventQueue.Enqueue(@event);
        }

        public void Send(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                EnrichEvent(@event);
                _eventQueue.Enqueue(@event);
            }
        }


        public void Send(Queue<IEvent> events)
        {
            while (events.Count > 0)
            {
                var @event = events.Dequeue();
                EnrichEvent(@event);
                _eventQueue.Enqueue(@event);
            }
        }

        private void EnrichEvent(IEvent @event)
        {
            if (_executionScope.CurrentScopeMetadata is null) return;

            @event.CorrelationId = _executionScope.CurrentScopeMetadata.CorrelationId;
            @event.CausationId = _executionScope.CurrentScopeMetadata.CausationId;
            @event.ApplicationName = _executionScope.CurrentScopeMetadata.ApplicationName;
            @event.ProcessingScope = _executionScope.CurrentScopeMetadata.ProcessingScope.Value;
        }
    }
}