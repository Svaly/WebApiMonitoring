using System;
using System.Runtime.Serialization;

namespace Framework.Messaging.Kafka.Exceptions
{
    [Serializable]
    public sealed class KafkaConnectionConfigNotFoundException : Exception
    {
        public KafkaConnectionConfigNotFoundException(string connectionName)
            : base($"Cannot find kafka connection config with given name {connectionName}")
        {
        }

        private KafkaConnectionConfigNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}