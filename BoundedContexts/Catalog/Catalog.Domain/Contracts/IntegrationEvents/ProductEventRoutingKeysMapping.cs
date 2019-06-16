using System;
using Catalog.Domain.Aggregates;
using Catalog.Domain.Events;
using Framework.Messaging.Publish;
using Framework.Patterns.Messaging;

namespace Catalog.Domain.Contracts.IntegrationEvents
{
    public sealed class UserEventRoutingKeysMapping : IEventRoutingKeysMapping<Product>
    {
        public string GetRoutingKey(IEvent @event)
        {
            if (@event is ProductAddedToCartEvent) return "Product.Added.To.Cart";

            if (@event is ProductLikedEvent) return "Product.Liked";

            throw new ArgumentException();
        }
    }
}