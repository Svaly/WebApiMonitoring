using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
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