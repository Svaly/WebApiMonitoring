using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.Logs.Logger
{
    public interface ILogsDispatcher
    {
        Task DispatchAsync(ILog log);
    }
}