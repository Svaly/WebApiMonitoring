using Framework.Messaging.Publish;
using Framework.Monitoring.Logs.Extensions;
using Framework.Monitoring.Logs.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Monitoring.Logs.Logger
{
    public class MessageQueueLogger : IMessageQueueLogger
    {
        private readonly IMessageQueuePublisher _messagePublisher;
        private readonly Queue<ILog> _logs;

        public MessageQueueLogger(IMessageQueuePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
            _logs = new Queue<ILog>();
        }

        public void EnqueueLog(ILog log)
        {
            _logs.Enqueue(log);
        }

        public Task CommitLogsAsync()
        {
            return _messagePublisher.PublishAsync("ServiceMonitoringPublishLogs", LogsToMessages());
        }

        private IEnumerable<KeyValuePair<string, string>> LogsToMessages()
        {
            while (_logs.Count > 0)
            {
                var log = _logs.Dequeue();
                yield return new KeyValuePair<string, string>(log.GetLogType().Value, log.ToJson());
            }
        }
    }
}
