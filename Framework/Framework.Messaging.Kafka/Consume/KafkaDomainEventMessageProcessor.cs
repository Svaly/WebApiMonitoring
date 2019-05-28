using System.Collections.Generic;
using Framework.Messaging.Consume;
using Framework.Patterns.Messaging;

namespace Framework.Messaging.Kafka.Consume
{
    public sealed class KafkaDomainEventMessageProcessor : IKafkaConsumedMessageProcessor
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IEventPublisher _eventPublisher;
        private readonly IEventRoutingEventsMappingProvider _eventRoutingEventsMappingProvider;

        public KafkaDomainEventMessageProcessor(
            IEventRoutingEventsMappingProvider eventRoutingEventsMappingProvider,
            IEventPublisher eventPublisher,
            IEventProcessor eventProcessor)
        {
            _eventRoutingEventsMappingProvider = eventRoutingEventsMappingProvider;
            _eventPublisher = eventPublisher;
            _eventProcessor = eventProcessor;
        }

        public void ProcessMessage(KeyValuePair<string, string> message, string connectionName)
        {
            var eventsMapping = _eventRoutingEventsMappingProvider.GetMapping(connectionName);
            var @event = eventsMapping.GetEvent(message.Key, message.Value);

            if (@event != null) _eventPublisher.Send(@event);

            _eventProcessor.Process();
        }
    }
}