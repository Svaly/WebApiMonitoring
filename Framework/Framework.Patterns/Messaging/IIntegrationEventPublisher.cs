using System.Collections.Generic;

namespace Framework.Patterns.Messaging
{
    public interface IIntegrationEventPublisher<TEntity>
        where TEntity : IAggregateRoot
    {
        void Publish(IEnumerable<IEvent> events);

        void Publish(IEvent @event);
    }
}