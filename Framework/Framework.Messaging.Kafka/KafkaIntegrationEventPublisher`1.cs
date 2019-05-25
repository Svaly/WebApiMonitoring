using Framework.Messaging.Converters;
using Framework.Messaging.Publish;
using Framework.Patterns;
using Framework.Patterns.Messaging;
using System.Collections.Generic;
using Framework.Patterns.Loging;

namespace Framework.Messaging.Kafka
{
    public sealed class KafkaIntegrationEventPublisher<TEntity> : IKafkaIntegrationEventPublisher<TEntity>
        where TEntity : IAggregateRoot
    {
        private readonly IIntegrationEventPublisherInMemoryMessageQueue _messageQueue;
        private readonly IEventRoutingKeysMapping<TEntity> _eventRoutingKeysMapping;
        private readonly IObjectSerializer _objectSerializer;
        private readonly IExecutionScope _executionScope;

        public KafkaIntegrationEventPublisher(
            IEventRoutingKeysMapping<TEntity> eventRoutingKeysMapping,
            IObjectSerializer objectSerializer,
            IIntegrationEventPublisherInMemoryMessageQueue messageQueue, IExecutionScope executionScope)
        {
            _eventRoutingKeysMapping = eventRoutingKeysMapping;
            _objectSerializer = objectSerializer;
            _messageQueue = messageQueue;
            _executionScope = executionScope;
        }

        public void Publish(IEvent @event)
        {
            EnrichEvent(@event);
            var message = CreateMessage(@event);
            _messageQueue.Enqueue(message);
        }

        public void Publish(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                EnrichEvent(@event);
                var message = CreateMessage(@event);
                _messageQueue.Enqueue(message);
            }
        }

        private KeyValuePair<string, string> CreateMessage(IEvent @event)
        {
            var routingKey = _eventRoutingKeysMapping.GetRoutingKey(@event);
            var body = _objectSerializer.SerializeToJsonString(@event);

            return new KeyValuePair<string, string>(routingKey, body);
        }

        private void EnrichEvent(IEvent @event)
        {
            @event.CorrelationId = _executionScope.CurrentScopeMetadata.CorrelationId;
            @event.CausationId = _executionScope.CurrentScopeMetadata.CausationId;
            @event.ApplicationName = _executionScope.CurrentScopeMetadata.ApplicationName;
            @event.ProcessingScope = _executionScope.CurrentScopeMetadata.ProcessingScope.Value;
        }
    }
}