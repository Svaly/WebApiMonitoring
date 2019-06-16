using System;
using Framework.Patterns.Ioc;
using Framework.Patterns.Loging;

namespace Framework.Patterns.Messaging
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IEventQueue _eventQueue;
        private readonly IExecutionScope _executionScope;
        private readonly IDependencyResolver _resolver;

        public EventProcessor(IEventQueue eventQueue, IDependencyResolver resolver, IExecutionScope executionScope)
        {
            _eventQueue = eventQueue;
            _resolver = resolver;
            _executionScope = executionScope;
        }

        public void Process()
        {
            while (_eventQueue.HasEvents)
            {
                var @event = _eventQueue.Dequeue();
                _executionScope.StartScope(@event);

                try
                {
                    HandleEvent(@event);
                }
                finally
                {
                    _executionScope.UnwindScope();
                }
            }
        }

        private void HandleEvent<T>(T @event)
            where T : IEvent
        {
            var eventType = @event.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            var handler = _resolver.GetService(handlerType);
            var handlerMethod = handler.GetType().GetMethod("Handle");
            handlerMethod?.Invoke(handler, new object[] {@event});
        }
    }
}