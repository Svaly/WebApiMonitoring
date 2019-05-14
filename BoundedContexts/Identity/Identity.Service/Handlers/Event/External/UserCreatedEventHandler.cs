﻿using Framework.Patterns.Messaging;
using Identity.Domain.Events;

namespace Identity.Service.Handlers.Event.External
{
    public sealed class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        public void Handle(UserCreatedEvent @event)
        {
            throw new System.NotImplementedException("UserCreatedEvent");
        }
    }
}
