using Framework.Patterns.Cqrs;
using Identity.Handlers.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Handlers.CommandHandler
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        public Task HandleAsync(RegisterUserCommand command)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                throw new NotImplementedException();
            });
        }
    }
}
