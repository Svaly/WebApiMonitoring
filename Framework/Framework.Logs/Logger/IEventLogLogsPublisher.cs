using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public interface IEventLogLogsPublisher
    {
        Task CommitLogAsync(ILog logs);
    }
}