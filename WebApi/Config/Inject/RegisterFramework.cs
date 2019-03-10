using Framework.Loging;
using Framework.Messaging;
using Framework.Service.Cqrs;
using Framework.Service.Cqrs.Implementation;
using SharedKernell;
using SimpleInjector;

namespace WebApi.Config.Inject
{
    public static class RegisterFramework
    {
        public static void Register(Container container)
        {
            container.Register<IDependencyResolver, DependencyResolver>(Lifestyle.Singleton);
            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Transient);
            container.Register<IApplicationMonitoringLogger, ApplicationMonitoringLogger>(Lifestyle.Scoped);
            container.Register<IMessagePublisher, KafkaMessagePublisher>(Lifestyle.Scoped);
        }
    }
}