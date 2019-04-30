using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public interface IFileLogsPublisher
    {
        Task CommitLogAsync(ILog logs);
    }
}