using Framework.Patterns.Loging;
using System.Collections.Concurrent;

namespace Framework.Logs.Logger
{
    public sealed class LogsQueue : ConcurrentQueue<ILog>, ILogsQueue
    {
    }
}