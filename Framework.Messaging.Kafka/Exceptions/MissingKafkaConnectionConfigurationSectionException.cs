using System;
using System.Runtime.Serialization;

namespace Framework.Messaging.Kafka.Exceptions
{
    [Serializable]
    public sealed class MissingKafkaConnectionConfigurationSectionException : Exception
    {
        public MissingKafkaConnectionConfigurationSectionException()
            : base("Missing kafka connection configuration section in config file")
        {
        }

        private MissingKafkaConnectionConfigurationSectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
