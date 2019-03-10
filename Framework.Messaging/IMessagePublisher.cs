using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync(IEnumerable<KeyValuePair<string, string>> messages, string connectionName);
    }
}
