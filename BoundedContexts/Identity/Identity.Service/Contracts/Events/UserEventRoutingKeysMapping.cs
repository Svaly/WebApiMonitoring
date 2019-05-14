using System;
using Framework.Messaging.Publish;
using Framework.Patterns.Messaging;
using Identity.Domain.Aggregates;
using Identity.Domain.Events;

namespace Identity.Service.Contracts.Events
{
    public sealed class UserEventRoutingKeysMapping : IEventRoutingKeysMapping<User>
    {
        public string GetRoutingKey(IEvent @event)
        {
            if (@event is UserCreatedEvent)
            {
                return "User.Created";
            }

            throw new ArgumentException();
        }
    }
}