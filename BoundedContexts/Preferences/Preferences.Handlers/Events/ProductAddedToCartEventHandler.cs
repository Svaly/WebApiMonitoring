using System;
using System.Diagnostics;
using Domain.Contracts.IntegrationEvents.Catalog;
using Framework.Patterns.Messaging;

namespace Preferences.Handlers.Events
{
    public sealed class ProductAddedToCartEventHandler : IEventHandler<ProductAddedToCartEvent>
    {
        public void Handle(ProductAddedToCartEvent @event)
        {
            Debug.WriteLine("ProductAddedToCartEventHandled");

            throw new ArgumentException("Product Added To Cart Event Handler");
        }
    }
}