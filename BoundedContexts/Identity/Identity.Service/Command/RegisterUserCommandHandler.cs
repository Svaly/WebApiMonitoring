using System;
using System.Threading.Tasks;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Messaging;
using Identity.Domain.Aggregates;
using Identity.Domain.Contracts.Commands;

namespace Identity.Handlers.Command
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
            return Task.Run(
                () =>
                {
                    throw new StackOverflowException("too much work");
                    //var user = new User(command.CommandId, Guid.NewGuid());
                    //_integrationEventPublisher.Publish(user.Events);
                });
        }
    }
}