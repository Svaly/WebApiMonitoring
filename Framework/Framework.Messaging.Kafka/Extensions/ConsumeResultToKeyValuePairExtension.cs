using Confluent.Kafka;
using System.Collections.Generic;

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
