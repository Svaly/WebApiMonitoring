using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Messaging.Publish;
using Framework.Monitoring.Logs.Extensions;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public class EventLogLogger : IEventLogLogger
    {
        private readonly IMessageQueuePublisher _messagePublisher;
        private readonly Queue<ILog> _logs;

        public EventLogLogger(IMessageQueuePublisher messagePublisher)
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
            return PublishToMessageQueue();
        }

        private Task PublishToMessageQueue()
        {
           return _messagePublisher.PublishAsync("kafkaLogsConnection", LogsToMessages());
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
