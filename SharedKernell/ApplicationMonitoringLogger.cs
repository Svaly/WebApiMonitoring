using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Framework.Loging;
using Framework.Loging.Extensions;
using Framework.Messaging;

namespace SharedKernell
{
    public class ApplicationMonitoringLogger : IApplicationMonitoringLogger
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly Queue<ILog> _logs;

        public ApplicationMonitoringLogger(IMessagePublisher messagePublisher)
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
           return _messagePublisher.PublishAsync(LogsToMessages(), "kafkaLogsConnection");
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
