using Framework.Logs.Logger;
using Framework.Messaging.Kafka.Consume;
using Framework.Monitoring;
using Framework.Patterns.Application;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Cqrs.Implementation;
using Framework.Patterns.Ioc;
using Framework.Patterns.Loging;
using Framework.Patterns.Messaging;
using SimpleInjector;
using WebApi.Messaging;

namespace WebApi.Config.Inject
{
    public static class RegisterFramework
    {
        public static void Register(Container container)
        {
            container.Register<IDependencyResolver>(() => new SimpleInjectorDependencyResolver(container), Lifestyle.Scoped);

            container.Register<IGlobalConfigurationProvider, GlobalConfigurationProvider>(Lifestyle.Singleton);
            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Transient);

            container.Register<IExecutionScope, ExecutionScope>(Lifestyle.Scoped);
            container.Register<IMonitoringLogsPublisher, MonitoringLogsPublisher>(Lifestyle.Scoped);

            container.Register<ILogsQueue, LogsQueue>(Lifestyle.Scoped);
            container.Register<ILogsDispatcher, LogsDispatcher>(Lifestyle.Scoped);
            container.Register<ILogsProcessor, LogsProcessor>(Lifestyle.Scoped);
            container.Register<ILogsPublisher, LogsPublisher>(Lifestyle.Scoped);
            container.Register<IEventLogLogsPublisher, EventLogLogsPublisher>(Lifestyle.Scoped);
            container.Register<IMessageQueueLogsPublisher, MessageQueueLogsPublisher>(Lifestyle.Scoped);

            container.Register<IEventQueue, InMemoryEventQueue>(Lifestyle.Scoped);
            container.Register<IEventPublisher, EventPublisher>(Lifestyle.Scoped);
            container.Register<IEventProcessor, EventProcessor>(Lifestyle.Scoped);
            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Scoped);
            //  container.RegisterDecorator<IEventDispatcher, EventDispatcherProxy>(Lifestyle.Singleton);

            container.Register<IKafkaConsumedMessageProcessor, KafkaConsumedIntegrationMessageProcessor>(Lifestyle.Scoped);
            container.RegisterDecorator<IKafkaConsumedMessageProcessor, KafkaIntegrationMessageProcessorProxy>(Lifestyle.Singleton);
        }
    }
}