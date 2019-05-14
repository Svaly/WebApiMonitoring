using Framework.Patterns.Cqrs;
using Framework.Patterns.Messaging;
using Identity.Domain.Aggregates;
using Identity.Service.Contracts.Command;
using System;
using System.Threading.Tasks;

namespace Identity.Service.Handlers.Command
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IIntegrationEventPublisher<User> _integrationEventPublisher;

        public RegisterUserCommandHandler(IIntegrationEventPublisher<User> integrationEventPublisher)
        {
            _integrationEventPublisher = integrationEventPublisher;
        }

        public Task HandleAsync(RegisterUserCommand command)
        {
            return Task.Run(() =>
            {
                var user = new User(command.CommandId, Guid.NewGuid());
                _integrationEventPublisher.Send(user.Events);
                //Thread.Sleep(2000);
                //throw new NotImplementedException();
            });
        }
    }
}
