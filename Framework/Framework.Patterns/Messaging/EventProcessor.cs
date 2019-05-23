using System;
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

                try
                {
                    HandleEvent(@event);
                    _executionScope.UnwindScope();
                }
                catch (Exception e)
                {
                    _executionScope.UnwindScope();
                    throw;
                }
            }
        }

        //Todo resolve not working
        private void HandleEvent<T>(T @event)
            where T : IEvent
        {
          //  var handler = _resolver.GetService<IEventHandler<T>>();
          var x = typeof(IEventHandler<T>);
            var handler = _resolver.GetService(x) as IEventHandler<T>;
            handler.Handle(@event);
        }
    }
}