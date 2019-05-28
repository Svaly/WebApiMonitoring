using System.Collections.Generic;
using Confluent.Kafka;

namespace Framework.Messaging.Kafka.Extensions
{
    internal static class ConsumeResultToKeyValuePairExtension
    {
        public static KeyValuePair<string, string> ToKeyValuePair(this ConsumeResult<string, string> consumeResult)
        {
            return new KeyValuePair<string, string>(consumeResult.Key, consumeResult.Value);
        }
    }
}