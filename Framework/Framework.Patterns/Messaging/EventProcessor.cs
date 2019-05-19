using Framework.Patterns.Ioc;
using Framework.Patterns.Loging;

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
                HandleEvent(@event);
                _executionScope.UnwindScope();
            }
        }

        private void HandleEvent<T>(T @event)
            where T : IEvent
        {
            var handler = _resolver.GetService<IEventHandler<T>>();
            handler.Handle(@event);
        }
    }
}