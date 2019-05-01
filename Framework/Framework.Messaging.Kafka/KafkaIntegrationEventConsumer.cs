using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Consume;
using Framework.Messaging.Kafka.Logs;

namespace Framework.Messaging.Kafka
{
    public sealed class KafkaIntegrationEventConsumer : IKafkaIntegrationEventConsumer
    {
        private readonly IKafkaConsumerMessageHandler _kafkaConsumerMessageHandler;
        private readonly IKafkaConsumer _kafkaConsumer;
        private readonly IKafkaConfigurationProvider _kafkaConfigurationProvider;
        private readonly ICollection<Task> _connectionsTasks;

        public KafkaIntegrationEventConsumer(IKafkaConsumerMessageHandler kafkaConsumerMessageHandler, IKafkaConsumer kafkaConsumer, IKafkaConfigurationProvider kafkaConfigurationProvider)
        {
            _kafkaConsumerMessageHandler = kafkaConsumerMessageHandler;
            _kafkaConsumer = kafkaConsumer;
            _kafkaConfigurationProvider = kafkaConfigurationProvider;
            _connectionsTasks = new List<Task>();
        }

        public ICollection<Task> ListenToAllEnabledConnections()
        {
            var connections = _kafkaConfigurationProvider.GetAllEnabledConsumeConnectionsConfigurations();

            foreach (var connection in connections)
            {
                _connectionsTasks.Add(Task.Run(() => _kafkaConsumer.ListenInfiniteLoop(connection as KafkaConnectionConfigModel, _kafkaConsumerMessageHandler)));
            }

            return _connectionsTasks;
        }
    }
}