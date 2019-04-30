using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public class FileLogsPublisher : IFileLogsPublisher
    {
        public Task CommitLogAsync(ILog logs)
        {
            return Task.CompletedTask;
        }
    }
}