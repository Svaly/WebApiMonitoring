using System;
using Framework.Patterns.Messaging;

namespace Domain.Contracts.IntegrationEvents.Catalog
{
    public sealed class ProductLikedEvent : Event
    {
        public ProductLikedEvent(Guid aggregateId, Guid? causedById) : base(aggregateId, causedById)
        {
        }
    }
}