using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
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
          while (_logsQueue.Count > 0)
          {
              if (_logsQueue.TryDequeue(out ILog log))
              {
                  await Task.WhenAll(
                      _messageQueueLogsPublisher.CommitLogAsync(log),
                      _eventLogLogsPublisher.CommitLogAsync(log),
                      _filesLogger.CommitLogAsync(log));
                }
          }
        }
    }
}
