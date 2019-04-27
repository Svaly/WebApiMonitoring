using Framework.Messaging;
using Framework.Monitoring.Logs.Logger;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Cqrs.Implementation;
using SimpleInjector;

namespace WebApi.Config.Inject
{
    public static class RegisterFramework
    {
        public static void Register(Container container)
        {
            container.Register<IDependencyResolver, DependencyResolver>(Lifestyle.Singleton);
            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Transient);
            container.Register<IEventLogLogger, EventLogLogger>(Lifestyle.Scoped);
            container.Register<IMessageQueueLogger, MessageQueueLogger>(Lifestyle.Scoped);
            container.Register<IFilesLogger, FilesLogger>(Lifestyle.Scoped);
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
        }
    }
}