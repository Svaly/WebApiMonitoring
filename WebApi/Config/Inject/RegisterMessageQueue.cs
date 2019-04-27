using Framework.Messaging.Converters;
using Framework.Messaging.Kafka.Configuration;
using Framework.Messaging.Kafka.Consume;
using Framework.Messaging.Kafka.Loging;
using Framework.Messaging.Kafka.Publish;
using Framework.Monitoring.Logs.Logger;
using SimpleInjector;

namespace WebApi.Config.Inject
{
    public static class RegisterMessageQueue
    {
        public static void Register(Container container)
        {
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
            container.Register<IKafkaLogger, KafkaLogger>(Lifestyle.Scoped);
            container.Register<IKafkaConfigurationProvider, KafkaConfigurationProvider>(Lifestyle.Scoped);
            container.Register<IKafkaProducerFactory, KafkaProducerFactory>(Lifestyle.Scoped);
            container.Register<IKafkaPublisher, KafkaPublisher>(Lifestyle.Scoped);
            container.Register<IObjectSerializer, ObjectSerializer>(Lifestyle.Scoped);
            container.Register<IKafkaConsumerFactory, KafkaConsumerFactory>(Lifestyle.Scoped);
            container.Register<IKafkaConsumer, KafkaConsumer>(Lifestyle.Scoped);
            container.Register<IKafkaConsumerMessageHandler, KafkaDomainEventMessageHandler>(Lifestyle.Scoped);
            container.Register<IObjectDeserializer, ObjectDeserializer>(Lifestyle.Scoped);
        }
    }
}