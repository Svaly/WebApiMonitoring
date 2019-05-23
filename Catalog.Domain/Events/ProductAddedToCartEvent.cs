using System;
using Framework.Patterns.Messaging;

namespace Catalog.Domain.Events
{
    public sealed class ProductAddedToCartEvent : Event
    {
        public ProductAddedToCartEvent(Guid aggregateId, Guid? causedById) : base(aggregateId, causedById)
        {
        }
    }
}
