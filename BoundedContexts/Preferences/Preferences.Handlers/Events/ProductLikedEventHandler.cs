using System.Diagnostics;
using Domain.Contracts.IntegrationEvents.Catalog;
using Framework.Patterns.Messaging;

namespace Preferences.Handlers.Events
{
    public sealed class ProductLikedEventHandler : IEventHandler<ProductLikedEvent>
    {
        public void Handle(ProductLikedEvent @event)
        {
            Debug.WriteLine("ProductLikedEvent Handled");
        }
    }
}