using System.Collections.Generic;
using Framework.Messaging.Kafka.Consume;

namespace WebApi.Messaging
{
    public class KafkaLogsMessagesHandler : IKafkaConsumerMessageHandler
    {
        public void HandleMessage(KeyValuePair<string, string> message, string connectionName)
        {
            throw new System.NotImplementedException();
        }
    }
}