using System.Collections.Generic;

namespace Framework.Patterns.Messaging
{
    public interface IEventPublisher
    {
        void Send(IEvent @event);

        void Send(Queue<IEvent> events);

        void Send(IEnumerable<IEvent> events);
    }
}