using Framework.Messaging.Publish;
using Framework.Monitoring.Logs.Extensions;
using Framework.Monitoring.Logs.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Monitoring.Logs.Publisher
{
    public class MessageQueueLogsPublisher : IMessageQueueLogsPublisher
    {
        private readonly IMessageQueuePublisher _messagePublisher;

        public MessageQueueLogsPublisher(IMessageQueuePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public Task CommitLogsAsync(IEnumerable<ILog> logs)
        {
            return _messagePublisher.PublishAsync("ServiceMonitoringPublishLogs", LogsToMessages(logs));
        }

        private IEnumerable<KeyValuePair<string, string>> LogsToMessages(IEnumerable<ILog> logs)
        {
            foreach (var log in logs)
            {
                yield return new KeyValuePair<string, string>(log.GetLogType().Value, log.ToJson());
            }
        }
    }
}
