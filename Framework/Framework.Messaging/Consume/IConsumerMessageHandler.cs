using System.Collections.Generic;

namespace Framework.Messaging.Consume
{
    public interface IConsumerMessageHandler
    {
        void HandleMessage(KeyValuePair<string, string> message, string connectionName);
    }
}