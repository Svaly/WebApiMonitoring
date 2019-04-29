using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Publisher
{
    public interface ILogsQueue
    {
        int Count { get; }

        void Enqueue(ILog log);

        bool TryDequeue(out ILog log);
    }
}