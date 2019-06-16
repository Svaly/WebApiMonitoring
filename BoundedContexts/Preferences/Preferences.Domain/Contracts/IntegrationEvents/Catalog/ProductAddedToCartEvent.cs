using System;
using Framework.Patterns.Messaging;

namespace Domain.Contracts.IntegrationEvents.Catalog
{
    public sealed class ProductAddedToCartEvent : Event
    {
        public ProductAddedToCartEvent(Guid aggregateId, Guid? causedById) : base(aggregateId, causedById)
        {
        }
    }
}