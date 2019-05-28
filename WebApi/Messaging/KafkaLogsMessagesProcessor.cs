using System.Collections.Generic;
using Framework.Messaging.Kafka.Consume;

namespace WebApi.Messaging
{
    public class KafkaLogsMessagesProcessor : IKafkaConsumedMessageProcessor
    {
        public void ProcessMessage(KeyValuePair<string, string> message, string connectionName)
        {
            //   throw new System.NotImplementedException();
        }
    }
}