using Framework.Monitoring.Logs.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Monitoring.Logs.Publisher
{
    public sealed class LogsPublisher : ILogsPublisher
    {
        private readonly ILogsQueue _logsQueue;

        private readonly IMessageQueueLogsPublisher _messageQueueLogsPublisher;

        private readonly IEventLogLogsPublisher _eventLogLogsPublisher;

        private readonly IFileLogsPublisher _filesLogger;

        public LogsPublisher(IEventLogLogsPublisher eventLogLogsPublisher, IMessageQueueLogsPublisher messageQueueLogsPublisher, IFileLogsPublisher filesLogger, ILogsQueue logsQueue)
        {
            _eventLogLogsPublisher = eventLogLogsPublisher;
            _messageQueueLogsPublisher = messageQueueLogsPublisher;
            _filesLogger = filesLogger;
            _logsQueue = logsQueue;
        }

        public async Task CommitLogsAsync()
        {
          var logs = GetLogs().ToList();

          await Task.WhenAll(
              _messageQueueLogsPublisher.CommitLogsAsync(logs.ToList()),
              _eventLogLogsPublisher.CommitLogsAsync(logs.ToList()),
              _filesLogger.CommitLogsAsync(logs.ToList()));
        }

        private IEnumerable<ILog> GetLogs()
        {
            while (_logsQueue.Count > 0)
            {
                if (_logsQueue.TryDequeue(out ILog log))
                {
                    yield return log;
                }
            }
        }
    }
}
