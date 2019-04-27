using Framework.Patterns;

namespace Framework.Messaging.Kafka
{
    public interface IKafkaIntegrationEventPublisher<TEntity> : IIntegrationEventPublisher<TEntity>
        where TEntity : IAggregateRoot
    {
    }
}