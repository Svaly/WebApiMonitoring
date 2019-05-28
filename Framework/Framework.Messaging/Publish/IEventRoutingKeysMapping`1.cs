using Framework.Patterns;
using Framework.Patterns.Messaging;

namespace Framework.Messaging.Publish
{
    public interface IEventRoutingKeysMapping<TEntity>
        where TEntity : IAggregateRoot
    {
        string GetRoutingKey(IEvent @event);
    }
}