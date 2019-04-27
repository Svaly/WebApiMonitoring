using System.Threading.Tasks;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public interface ILogger
    {
        Task CommitLogsAsync();

        void EnqueueLog(ILog log);
    }
}
