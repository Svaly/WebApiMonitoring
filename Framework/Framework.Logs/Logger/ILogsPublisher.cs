using System.Threading.Tasks;

namespace Framework.Logs.Logger
{
    public interface ILogsPublisher
    {
        Task CommitLogsAsync();
    }
}
