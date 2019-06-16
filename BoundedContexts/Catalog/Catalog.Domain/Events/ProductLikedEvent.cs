using System;
using Framework.Patterns.Messaging;

namespace Catalog.Domain.Events
{
    public sealed class ProductLikedEvent : Event
    {
        public ProductLikedEvent(Guid aggregateId, Guid? causedById) : base(aggregateId, causedById)
        {
        }
    }
}