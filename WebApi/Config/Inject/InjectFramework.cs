using Framework.Service.Cqrs;
using Framework.Service.Cqrs.Implementation;
using Identity.Handlers.Command;
using Identity.Handlers.CommandHandler;
using SimpleInjector;

namespace WebApi.Config.Inject
{
    public static class InjectFramework
    {
        public static void Register(Container container)
        {
            //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            // container.Register(typeof(ICommand), typeof(ICommandHandler<ICommand>).Assembly);
            // container.Register(typeof(ICommandHandler<>), typeof(ICommandHandler<>).Assembly);
            container.Register<IDependencyResolver, DependencyResolver>(Lifestyle.Singleton);
            container.Register<ICommandDispatcher, CommandDispatcher>();


            container.Register<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();
        }
    }
}