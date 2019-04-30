using Framework.Logs.Logger;
using Framework.Monitoring;
using Framework.Patterns.Application;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Cqrs.Implementation;
using Framework.Patterns.Loging;
using SimpleInjector;

namespace WebApi.Config.Inject
{
    public static class RegisterFramework
    {
        public static void Register(Container container)
        {
            container.Register<IDependencyResolver, DependencyResolver>(Lifestyle.Singleton);
            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Transient);
            container.Register<IGlobalConfigurationProvider, GlobalConfigurationProvider>(Lifestyle.Scoped);
            container.Register<ILogsQueue, LogsQueue>(Lifestyle.Scoped);
            container.Register<ILogger, Logger>(Lifestyle.Scoped);

            container.Register<IExecutionScopeMetadata, ExecutionScopeMetadata>(Lifestyle.Scoped);
            container.Register<IMonitoringLogger, MonitoringLogger>(Lifestyle.Scoped);

            container.Register<IEventLogLogsPublisher, EventLogLogsPublisher>(Lifestyle.Scoped);
            container.Register<IMessageQueueLogsPublisher, MessageQueueLogsPublisher>(Lifestyle.Scoped);
            container.Register<IFileLogsPublisher, FileLogsPublisher>(Lifestyle.Scoped);
            container.Register<ILogsPublisher, LogsPublisher>(Lifestyle.Scoped);
        }
    }
}