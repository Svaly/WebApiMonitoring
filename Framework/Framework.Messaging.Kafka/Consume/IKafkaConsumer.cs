using System.Threading.Tasks;
using Framework.Messaging.Consume;
using Framework.Messaging.Kafka.Configuration;

namespace Framework.Messaging.Kafka.Consume
{
    public interface IKafkaConsumer : IMessageQueueConsumer
    {
        Task ListenInfiniteLoopAsync(
            KafkaConnectionConfigModel connectionConfig,
            IKafkaConsumedMessageProcessor kafkaConsumedMessageProcessor);
    }
}