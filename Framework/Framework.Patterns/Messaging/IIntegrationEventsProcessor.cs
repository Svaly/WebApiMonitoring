using System.Threading.Tasks;

namespace Framework.Patterns.Messaging
{
    public interface IIntegrationEventsProcessor
    {
        Task ProcessAsync(string connectionName = null);
    }
}