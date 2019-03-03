using System.Threading.Tasks;

namespace Framework.Service.Cqrs.Implementation
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public CommandDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task Handle<T>(T command) where T : ICommand
        {
            var handler = (ICommandHandler<T>)_resolver.GetService(typeof(ICommandHandler<T>));
            await handler.Execute(command);
        }
    }
}
