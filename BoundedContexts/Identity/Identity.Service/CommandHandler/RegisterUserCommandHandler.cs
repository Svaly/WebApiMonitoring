using System;
using Identity.Handlers.Command;
using System.Threading;
using System.Threading.Tasks;
using Framework.Service.Cqrs;

namespace Identity.Handlers.CommandHandler
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        public Task Execute(RegisterUserCommand command)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                throw new NotImplementedException();
            });
        }
    }
}
