using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Framework.Logs.Logger;
using Framework.Messaging.Converters;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Consume;
using Framework.Messaging.Kafka.Logs;
using Framework.Messaging.Kafka.Publish;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace WebApi.Messaging
{
    public static class KafkaMessageQueueConsumersRegistrar
    {
        private static readonly IKafkaConfigurationProvider _kafkaConfigurationProvider = new KafkaConfigurationProvider();
        private static readonly ICollection<Task> _connectionsTasks = new List<Task>();

        public static void RegisterConsumeConnection<T>(Container dependencyContainer, string connectionName)
            where T : class, IKafkaConsumedMessageProcessor
        {
            var connection =
                _kafkaConfigurationProvider.GetConsumeConnectionConfiguration(connectionName) as
                    KafkaConnectionConfigModel;

            Debug.WriteLine(connection.ConnectionIsEnabled);

            if (connection.ConnectionIsEnabled)
                _connectionsTasks.Add(
                    Task.Run(
                        () =>
                        {
                            using (var scope = AsyncScopedLifestyle.BeginScope(dependencyContainer))
                            {
                                var consumer = scope.GetInstance<IKafkaConsumer>();
                                var kafkaConsumerMessageHandler = scope.GetInstance<T>();
                                consumer.ListenInfiniteLoopAsync(connection, kafkaConsumerMessageHandler);
                            }
                        }));
        }

        //private static IKafkaConsumer CreateConsumer()
        //{
        //    var logsQueue = new LogsQueue();
        //    var publishLogs = new LogsQueue();

        //    return new
        //        KafkaConsumer(
        //            new KafkaConsumerFactory(),
        //            new KafkaLogger(new LogsPublisher(logsQueue)),
        //            new LogsProcessor(
        //                logsQueue,
        //                new LogsDispatcher(
        //                    new EventLogLogsPublisher(),
        //                    new MessageQueueLogsPublisher(
        //                        new KafkaPublisher(
        //                            new KafkaPublisherFactory(),
        //                            new KafkaLogger(new LogsPublisher(new LogsQueue())),
        //                            new KafkaConfigurationProvider()),
        //                        new ObjectSerializer()))));
        //}
    }
}