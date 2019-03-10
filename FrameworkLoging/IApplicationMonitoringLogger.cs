using System.Threading.Tasks;

namespace Framework.Loging
{
    public interface IApplicationMonitoringLogger
    {
        Task CommitLogsAsync();

        void EnqueueLog(ILog log);
    }
}