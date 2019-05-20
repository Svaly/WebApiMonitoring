//using Framework.Patterns.Messaging;
//using SimpleInjector;
//using SimpleInjector.Lifestyles;
//using System;

//namespace WebApi.Messaging
//{
//    public class EventDispatcherProxy : IEventDispatcher
//    {
//        private readonly Container _container;
//        private readonly Func<IEventDispatcher> _eventDispatcherFactory;

//        public EventDispatcherProxy(Container container, Func<IEventDispatcher> eventDispatcherFactory)
//        {
//            _container = container;
//            _eventDispatcherFactory = eventDispatcherFactory;
//        }

//        public void Dispatch(IEvent @event)
//        {
//            using (AsyncScopedLifestyle.BeginScope(_container))
//            {
//                var eventDispatcher = _eventDispatcherFactory();
//                eventDispatcher.Dispatch(@event);
//            }
//        }
//    }
//}