using System.Collections.Concurrent;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public sealed class LogsQueue : ConcurrentQueue<ILog>, ILogsQueue
    {
    }
}