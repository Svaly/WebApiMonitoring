using System.Threading.Tasks;

namespace Framework.Monitoring.Logs.Publisher
{
    public interface ILogsPublisher
    {
        Task CommitLogsAsync();
    }
}
