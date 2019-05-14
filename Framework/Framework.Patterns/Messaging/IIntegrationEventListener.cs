using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Patterns.Messaging
{
    public interface IIntegrationEventListener
    {
        ICollection<Task> ListenToAllEnabledConnections();
    }
}