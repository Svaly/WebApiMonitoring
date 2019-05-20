using System.Threading.Tasks;

namespace Framework.Patterns.Loging
{
    public interface ILogsProcessor
    {
        Task ProcessAsync();
    }
}