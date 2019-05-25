using Framework.Patterns.Ioc;
using Framework.Patterns.Loging;
using System;

namespace Framework.Patterns.Messaging
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IEventQueue _eventQueue;
        private readonly IDependencyResolver _resolver;
        private readonly IExecutionScope _executionScope;

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
                catch (Exception e)
                {
                    _executionScope.UnwindScope();
                    throw;
                }

                _executionScope.UnwindScope();
            }
        }

        private void HandleEvent<T>(T @event)
            where T : IEvent
        {
            var eventType = @event.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            var handler = _resolver.GetService(handlerType);
            var handlerMethod = handler.GetType().GetMethod("Handle");
            handlerMethod?.Invoke(handler, new object[] { @event });
        }
    }
}