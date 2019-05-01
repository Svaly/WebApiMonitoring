using Framework.Patterns.Ioc;

namespace Framework.Patterns.Messaging
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IEventQueue _eventQueue;
        private readonly IDependencyResolver _resolver;

        public EventProcessor(IEventQueue eventQueue, IDependencyResolver resolver)
        {
            _eventQueue = eventQueue;
            _resolver = resolver;
        }

        public void Process()
        {
            while (_eventQueue.HasEvents)
            {
                HandleEvent(_eventQueue.Dequeue());
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