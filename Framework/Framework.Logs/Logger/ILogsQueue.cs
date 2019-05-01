using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public interface ILogsQueue
    {
        bool HasEvents { get; }

        void Enqueue(ILog log);

        bool TryDequeue(out ILog log);
    }
}