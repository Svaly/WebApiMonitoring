namespace Framework.Messaging.Kafka.Consume
{
    public class ConsumeResult
    {
        private readonly Confluent.Kafka.ConsumeResult<string, string> _consumeResult;

        public ConsumeResult(Confluent.Kafka.ConsumeResult<string, string> consumeResult)
        {
            _consumeResult = consumeResult;
        }

        // Summary:
        //     The topic associated with the message.
        public string Topic => _consumeResult.Topic;

        // Summary:
        //     The partition associated with the message.
        public int Partition => _consumeResult.Partition;

        // Summary:
        //     The partition offset associated with the message.
        public long Offset => _consumeResult.Offset;

        // Summary:
        //     The TopicPartition associated with the message.
        public int TopicPartition => _consumeResult.TopicPartition.Partition;

        // Summary:
        //     The TopicPartitionOffset assoicated with the message.
        public long TopicPartitionOffset => _consumeResult.TopicPartitionOffset.Offset;

        // Summary:
        //     The Kafka message, or null if this ConsumeResult instance represents an end of
        //     partition event.
        public Confluent.Kafka.Message<string, string> Message => _consumeResult.Message;

        // Summary:
        //     The Kafka message Key.
        public string Key => _consumeResult.Key;

        // Summary:
        //     The Kafka message Value.
        public string Value => _consumeResult.Value;

        // Summary:
        //     The Kafka message timestamp.
        public Confluent.Kafka.Timestamp Timestamp => _consumeResult.Timestamp;

        // Summary:
        //     The Kafka message headers.
        public Confluent.Kafka.Headers Headers => _consumeResult.Headers;

        // Summary:
        //     True if this instance represents an end of partition event, false if it represents
        //     a message in kafka.
        public bool IsPartitionEOF => _consumeResult.IsPartitionEOF;
    }
}
