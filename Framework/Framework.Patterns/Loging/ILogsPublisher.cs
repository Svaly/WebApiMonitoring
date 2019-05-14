using System.Threading.Tasks;

namespace Framework.Patterns.Loging
{
    public interface ILogsPublisher
    {
        Task CommitLogsAsync();
    }
}
