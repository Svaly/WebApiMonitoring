using System;
using System.Threading.Tasks;
using Framework.Patterns.Ioc;

namespace Framework.Patterns.Cqrs.Implementation
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public CommandDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task DispatchAsync<T>(T command)
            where T : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command), "Command can not be null.");

            var handler = _resolver.GetService<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}