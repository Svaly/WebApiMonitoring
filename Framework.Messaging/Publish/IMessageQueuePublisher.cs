using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Messaging.Publish
{
    public interface IMessageQueuePublisher
    {
        Task PublishAsync(string connectionName, IEnumerable<KeyValuePair<string, string>> messages);

        Task PublishAsync(string connectionName, KeyValuePair<string, string> message);
    }
}