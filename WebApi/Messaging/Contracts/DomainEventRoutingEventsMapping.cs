using System;
using Domain.Contracts.IntegrationEvents.Catalog;
using Framework.Messaging.Consume;
using Framework.Messaging.Converters;
using Framework.Patterns.Messaging;
using Identity.Domain.Events;

namespace WebApi.Messaging.Contracts
{
    public class DomainEventRoutingEventsMapping : IEventRoutingEventsMapping
    {
        private readonly IObjectDeserializer _objectDeserializer;

        public DomainEventRoutingEventsMapping(IObjectDeserializer objectDeserializer)
        {
            _objectDeserializer = objectDeserializer;
        }

        public IEvent GetEvent(string routingKey, string message)
        {
            switch (routingKey)
            {
                case "User.Created":
                    return _objectDeserializer.Deserialize<UserCreatedEvent>(message);
                case "Product.Added.To.Cart":
                    return _objectDeserializer.Deserialize<ProductAddedToCartEvent>(message);
                case "Product.Liked":
                    return _objectDeserializer.Deserialize<ProductLikedEvent>(message);
                default:
                    throw new ArgumentException($"No type binding available for given routing key {routingKey}");
            }
        }
    }
}