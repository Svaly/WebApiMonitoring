using System;
using System.Linq;

namespace Framework.Messaging.Kafka.Logs
{
    public sealed class KafkaLogType
    {
        private static readonly string _consume = "Consume";
        private static readonly string _publish = "Publish";

        private static readonly string[] AvailableTypes=
        {
            _consume, _publish
        };

        public KafkaLogType(string kafkaLogType)
        {
            Validate(kafkaLogType);
            Value = kafkaLogType;
        }

        public static KafkaLogType Consume => new KafkaLogType(_consume);

        public static KafkaLogType Publish => new KafkaLogType(_publish);

        public string Value { get; }

        private void Validate(string kafkaLogType)
        {
            if (string.IsNullOrEmpty(kafkaLogType))
            {
                throw new ArgumentNullException(kafkaLogType, "Kafka log type scope cannot be null");
            }

            if (!AvailableTypes.Contains(kafkaLogType))
            {
                throw new ArgumentException($"Invalid log type: {kafkaLogType}");
            }
        }
    }
}