using Framework.Patterns.Ioc;

namespace Framework.Patterns.Messaging
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public EventDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public void Dispatch(IEvent @event)
        {
            DispatchEvent(@event);
        }

        private void DispatchEvent<T>(T @event)
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