using System.Collections.Generic;

namespace Framework.Patterns.Messaging
{
    public interface IIntegrationEventPublisher<TEntity>
    {
        void Send(IEnumerable<IEvent> events);

        void Send(IEvent @event);

        void SetConnectionName(string connectionName);
    }
}