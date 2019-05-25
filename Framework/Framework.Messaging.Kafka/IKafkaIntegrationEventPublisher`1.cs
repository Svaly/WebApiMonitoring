using Framework.Patterns;
using Framework.Patterns.Messaging;

namespace Framework.Messaging.Kafka
{
    public interface IKafkaIntegrationEventPublisher<TEntity> : IIntegrationEventPublisher<TEntity>
        where TEntity : IAggregateRoot
    {
    }
}