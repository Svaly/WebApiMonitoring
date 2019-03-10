using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Framework.Messaging
{
    public sealed class KafkaMessagePublisher : IMessagePublisher
    {
        public Task PublishAsync(IEnumerable<KeyValuePair<string, string>> messages, string connectionName)
        {
           return Task.Factory.StartNew(() =>
            {
                foreach (var message in messages)
                {
                    Debug.WriteLine($"key: {message.Key}, value: {message.Value}");
                }
            });
        }
    }
}
