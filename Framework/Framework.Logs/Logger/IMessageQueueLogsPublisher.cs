using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public interface IMessageQueueLogsPublisher
    {
        Task CommitLogAsync(ILog log);
    }
}