using System;
using Framework.Messaging.Consume;
using Framework.Messaging.Converters;
using Framework.Patterns.Messaging;
using Identity.Domain.Events;

namespace WebApi.Messaging
{
    public class EventRoutingEventsMapping : IEventRoutingEventsMapping
    {
        private readonly IObjectDeserializer _objectDeserializer;

        public EventRoutingEventsMapping(IObjectDeserializer objectDeserializer)
        {
            _objectDeserializer = objectDeserializer;
        }

        public IEvent GetEvent(string routingKey, string message)
        {
            switch (routingKey)
            {
                case "User.Created":
                    return _objectDeserializer.Deserialize<NewUserEvent>(message);
                default:
                    throw new ArgumentException($"No type binding available for given routing key {routingKey}");
            }
        }
    }
}