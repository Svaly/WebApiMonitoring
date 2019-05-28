using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Messaging.Kafka.Publish;
using Framework.Messaging.Publish;

namespace Framework.Messaging.Kafka
{
    public sealed class KafkaIntegrationEventsProcessor : IKafkaIntegrationEventsProcessor
    {
        private readonly string _defaultPublishConnectionName;
        private readonly IKafkaPublisher _kafkaPublisher;
        private readonly IIntegrationEventPublisherInMemoryMessageQueue _messageQueue;

        public KafkaIntegrationEventsProcessor(
            IDefaultPublishConnectionNameProvider defaultPublishConnectionNameProvider,
            IIntegrationEventPublisherInMemoryMessageQueue messageQueue,
            IKafkaPublisher kafkaPublisher)
        {
            _messageQueue = messageQueue;
            _kafkaPublisher = kafkaPublisher;
            _defaultPublishConnectionName = defaultPublishConnectionNameProvider.GetDefaultPublishConnectionName();
        }

        public async Task ProcessAsync(string connectionName = null)
        {
            var publishConnectionName = connectionName ?? _defaultPublishConnectionName;

            var messages = GetMessagesFromQueue().ToList();
            await _kafkaPublisher.PublishAsync(publishConnectionName, messages);
        }

        private IEnumerable<KeyValuePair<string, string>> GetMessagesFromQueue()
        {
            var messages = new Queue<KeyValuePair<string, string>>();

            while (_messageQueue.TryDequeue(out var message)) messages.Enqueue(message);

            return messages;
        }
    }
}