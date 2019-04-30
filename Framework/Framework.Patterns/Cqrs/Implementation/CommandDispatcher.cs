using System;
using System.Threading.Tasks;

namespace Framework.Patterns.Cqrs.Implementation
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public CommandDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),"Command can not be null.");
            }

            var handler = (ICommandHandler<T>)_resolver.GetService(typeof(ICommandHandler<T>));
            await handler.HandleAsync(command);
        }
    }
}
