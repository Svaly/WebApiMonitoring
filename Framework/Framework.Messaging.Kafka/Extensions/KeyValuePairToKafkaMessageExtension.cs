using Confluent.Kafka;
using System.Collections.Generic;

namespace Framework.Messaging.Kafka.Extensions
{
    internal static class KeyValuePairToKafkaMessageExtension
    {
        public static Message<string, string> ToKafkaMessage(this KeyValuePair<string, string> message)
        {
            return new Message<string, string>
            {
                Key = message.Key,
                Value = message.Value
            };
        }
    }
}