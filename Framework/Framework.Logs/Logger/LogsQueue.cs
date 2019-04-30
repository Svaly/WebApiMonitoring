using System.Collections.Concurrent;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public sealed class LogsQueue : ILogsQueue
    {
        private readonly ConcurrentQueue<ILog> _logs;

        public LogsQueue()
        {
            _logs = new ConcurrentQueue<ILog>();
        }

        public int Count => _logs.Count;

        public void Enqueue(ILog log)
        {
            _logs.Enqueue(log);
        }

        public bool TryDequeue(out ILog log)
        {
            return _logs.TryDequeue(out log);
        }
    }
}