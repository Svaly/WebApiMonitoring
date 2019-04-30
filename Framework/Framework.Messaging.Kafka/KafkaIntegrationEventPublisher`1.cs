using Framework.Messaging.Converters;
using Framework.Messaging.Kafka.Publish;
using Framework.Messaging.Publish;
using Framework.Patterns;
using System.Collections.Generic;
using System.Linq;
using Framework.Patterns.Messaging;

namespace Framework.Messaging.Kafka
{
    public sealed class KafkaIntegrationEventPublisher<TEntity> : IKafkaIntegrationEventPublisher<TEntity>
        where TEntity : IAggregateRoot
    {
        private readonly IEventRoutingKeysMapping<TEntity> _eventRoutingKeysMapping;
        private readonly IKafkaPublisher _kafkaPublisher;
        private readonly IObjectSerializer _objectSerializer;
        private string _connectionName;

        public KafkaIntegrationEventPublisher(
            IEventRoutingKeysMapping<TEntity> eventRoutingKeysMapping,
            IKafkaPublisher kafkaPublisher,
            IObjectSerializer objectSerializer,
            IDefaultPublishConnectionNameProvider defaultPublishConnectionNameProvider)
        {
            _eventRoutingKeysMapping = eventRoutingKeysMapping;
            _kafkaPublisher = kafkaPublisher;
            _objectSerializer = objectSerializer;
            _connectionName = defaultPublishConnectionNameProvider.GetDefaultPublishConnectionName();
        }

        public void SetConnectionName(string connectionName)
        {
            _connectionName = connectionName;
        }

        public void Send(IEnumerable<IEvent> events)
        {
            _kafkaPublisher.PublishAsync(_connectionName, events.Select(CreateMessage));
        }

        public void Send(IEvent @event)
        {
            _kafkaPublisher.PublishAsync(_connectionName, CreateMessage(@event));
        }

        private KeyValuePair<string, string> CreateMessage(IEvent @event)
        {
            var routingKey = _eventRoutingKeysMapping.GetRoutingKey(@event);
            var body = _objectSerializer.SerializeToJsonString(@event);

            return new KeyValuePair<string, string>(routingKey, body);
        }
    }
}