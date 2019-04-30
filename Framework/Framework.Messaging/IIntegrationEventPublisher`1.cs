using Framework.Patterns;
using System.Collections.Generic;
using Framework.Patterns.Messaging;

namespace Framework.Messaging
{
    public interface IIntegrationEventPublisher<TEntity>
    {
        void Send(IEnumerable<IEvent> events);

        void Send(IEvent @event);

        void SetConnectionName(string connectionName);
    }
}