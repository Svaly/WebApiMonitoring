using Framework.Messaging.Consume;
using Framework.Messaging.Converters;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Consume;
using Framework.Messaging.Kafka.Logs;
using Framework.Messaging.Kafka.Publish;
using Framework.Messaging.Publish;
using SimpleInjector;
using WebApi.Messaging;
using WebApi.Messaging.Contracts;

namespace WebApi.Config.Inject
{
    public static class RegisterMessageQueue
    {
        public static void Register(Container container)
        {
            container.Register<IKafkaLogger, KafkaLogger>(Lifestyle.Scoped);
            container.Register<IKafkaConfigurationProvider, KafkaConfigurationProvider>(Lifestyle.Scoped);
            container.Register<IKafkaProducerFactory, KafkaProducerFactory>(Lifestyle.Scoped);
            container.Register<IKafkaPublisher, KafkaPublisher>(Lifestyle.Scoped);
            container.Register<IMessageQueuePublisher, KafkaPublisher>(Lifestyle.Scoped);
            container.Register<IObjectSerializer, ObjectSerializer>(Lifestyle.Scoped);
            container.Register<IKafkaConsumerFactory, KafkaConsumerFactory>(Lifestyle.Scoped);
            container.Register<IKafkaConsumer, KafkaConsumer>(Lifestyle.Scoped);
            container.Register<IObjectDeserializer, ObjectDeserializer>(Lifestyle.Scoped);

            container.Register<IEventRoutingEventsMappingProvider, EventRoutingEventsMappingProvider>(Lifestyle.Scoped);
            container.Register<KafkaConsumedIntegrationMessageProcessor>(Lifestyle.Scoped);
            container.Register<KafkaLogsMessagesProcessor>(Lifestyle.Scoped);

            container.Register<IDefaultPublishConnectionNameProvider, DefaultPublishConnectionNameProvider>(Lifestyle.Scoped);
            container.Register<IEventRoutingEventsMapping, DomainEventRoutingEventsMapping>(Lifestyle.Scoped);
        }
    }
}