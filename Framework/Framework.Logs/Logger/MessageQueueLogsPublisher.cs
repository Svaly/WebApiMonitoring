using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Messaging.Converters;
using Framework.Messaging.Publish;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public class MessageQueueLogsPublisher : IMessageQueueLogsPublisher
    {
        private readonly IMessageQueuePublisher _messagePublisher;
        private readonly IObjectSerializer _serializer;

        public MessageQueueLogsPublisher(
            IMessageQueuePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public Task CommitLogAsync(ILog log)
        {
            return _messagePublisher.PublishAsync("ServiceMonitoringPublishLogs", new KeyValuePair<string, string>(log.GetType().ToString(), _serializer.SerializeToJsonString(log)));
        }
    }
}
