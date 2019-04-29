using Framework.Monitoring.Logs.Publisher;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public sealed class Logger : ILogger
    {
        private readonly ILogsQueue _logsQueue;

        public Logger(ILogsQueue logsQueue)
        {
            _logsQueue = logsQueue;
        }

        public void Log(ILog log)
        {
            _logsQueue.Enqueue(log);
        }
    }
}