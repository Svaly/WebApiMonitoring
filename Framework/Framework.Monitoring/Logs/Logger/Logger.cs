using System.Threading.Tasks;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public sealed class Logger : ILogger
    {
        private readonly IMessageQueueLogger _messageQueueLogger;

        private readonly IEventLogLogger _eventLogLogger;

        private readonly IFilesLogger _filesLogger;

        public Logger(IEventLogLogger eventLogLogger, IMessageQueueLogger messageQueueLogger, IFilesLogger filesLogger)
        {
            _eventLogLogger = eventLogLogger;
            _messageQueueLogger = messageQueueLogger;
            _filesLogger = filesLogger;
        }

        public async Task CommitLogsAsync()
        {
          await Task.WhenAll(
              _messageQueueLogger.CommitLogsAsync(),
              _eventLogLogger.CommitLogsAsync(),
              _filesLogger.CommitLogsAsync());
        }

        public void EnqueueLog(ILog log)
        {
            _messageQueueLogger.EnqueueLog(log);
            _eventLogLogger.EnqueueLog(log);
            _filesLogger.EnqueueLog(log);
        }
    }
}
