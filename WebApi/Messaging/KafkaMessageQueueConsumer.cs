using Framework.Logs.Logger;
using Framework.Messaging.Kafka.Consume;
using Framework.Messaging.Kafka.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Messaging.Kafka.Configuration;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace WebApi.Messaging
{
    public static class KafkaMessageQueueConsumer
    {
        private static readonly IKafkaConfigurationProvider _kafkaConfigurationProvider = new KafkaConfigurationProvider();
        private static readonly ICollection<Task> _connectionsTasks = new List<Task>();

        public static void RegisterConsumeConnection<T>(Container dependencyContainer, string connectionName)
            where T : class, IKafkaConsumerMessageHandler
        {
            var connection = _kafkaConfigurationProvider.GetConsumeConnectionConfiguration(connectionName) as KafkaConnectionConfigModel;

            if (!connection.ConnectionIsEnabled)
            {
                return;
            }

            _connectionsTasks.Add(Task.Run(() =>
            {
                using (var scope = AsyncScopedLifestyle.BeginScope(dependencyContainer))
                {
                    var consumer = scope.GetInstance<IKafkaConsumer>();
                    var kafkaConsumerMessageHandler = scope.GetInstance<T>();
                    consumer.ListenInfiniteLoop(connection, kafkaConsumerMessageHandler);
                }
            }));
        }

        //private static IKafkaConsumer CreateConsumer()
        //{
        ////    return new KafkaConsumer(new KafkaConsumerFactory(), new KafkaLogger(new Logger(new LogsQueue())), new LogsPublisher(new));
        //}

    }
}