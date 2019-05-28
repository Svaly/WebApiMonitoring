using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public sealed class LogsProcessor : ILogsProcessor
    {
        private readonly ILogsDispatcher _logsDispatcher;
        private readonly ILogsQueue _logsQueue;

        public LogsProcessor(ILogsQueue logsQueue, ILogsDispatcher logsDispatcher)
        {
            _logsQueue = logsQueue;
            _logsDispatcher = logsDispatcher;
        }

        public async Task ProcessAsync()
        {
            while (_logsQueue.TryDequeue(out var log)) await _logsDispatcher.DispatchAsync(log);
        }
    }
}