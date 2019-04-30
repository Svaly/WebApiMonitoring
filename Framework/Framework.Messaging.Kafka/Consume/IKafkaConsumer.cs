using Framework.Messaging.Consume;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Consume
{
    public interface IKafkaConsumer : IMessageQueueConsumer
    {
        void ListenInfiniteLoop(KafkaConnectionConfigModel connectionConfig, IKafkaConsumerMessageHandler kafkaConsumerMessageHandler);
    }
}
