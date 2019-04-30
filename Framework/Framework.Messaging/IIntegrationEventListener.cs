using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Messaging
{
    public interface IIntegrationEventConsumer
    {
        ICollection<Task> ListenToAllEnabledConnections();
    }
}