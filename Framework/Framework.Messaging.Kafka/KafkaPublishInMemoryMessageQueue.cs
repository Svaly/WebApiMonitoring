using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Framework.Messaging.Kafka
{
    public sealed class KafkaPublishInMemoryMessageQueue : ConcurrentQueue<KeyValuePair<string, string>>,
        IKafkaPublishInMemoryMessageQueue
    {
    }
}