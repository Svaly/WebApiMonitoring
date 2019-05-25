using System;
using System.Threading.Tasks;
using Catalog.Domain.Aggregates;
using Catalog.Domain.Contracts.Commands;
using Catalog.Domain.Events;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Messaging;

namespace Catalog.Handlers.Command
{
    public sealed class LikeProductCommandHandler : ICommandHandler<LikeProductCommand>
    {
        private readonly IIntegrationEventPublisher<Product> _integrationEventPublisher;

        public LikeProductCommandHandler(IIntegrationEventPublisher<Product> integrationEventPublisher)
        {
            _integrationEventPublisher = integrationEventPublisher;
        }

        public Task HandleAsync(LikeProductCommand command)
        {
            return Task.Run(() =>
            {
                var @event = new ProductLikedEvent(command.CommandId, Guid.NewGuid());
                _integrationEventPublisher.Publish(@event);
            });
        }
    }
}