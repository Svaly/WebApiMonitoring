using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public interface ILogsQueue
    {
        int Count { get; }

        void Enqueue(ILog log);

        bool TryDequeue(out ILog log);
    }
}