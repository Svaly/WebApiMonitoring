using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public sealed class LogsPublisher : ILogsPublisher
    {
        private readonly ILogsQueue _logsQueue;

        public LogsPublisher(ILogsQueue logsQueue)
        {
            _logsQueue = logsQueue;
        }

        public void Publish(ILog log)
        {
            _logsQueue.Enqueue(log);
        }
    }
}