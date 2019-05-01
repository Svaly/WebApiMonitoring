using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.Patterns.Cqrs;
using Identity.Service.Commands;

namespace Identity.Service.CommandHandler
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
