using System.Collections.Generic;

namespace Framework.Messaging.Consume
{
    public interface IConsumedMessageProcessor
    {
        void ProcessMessage(KeyValuePair<string, string> message, string connectionName);
    }
}