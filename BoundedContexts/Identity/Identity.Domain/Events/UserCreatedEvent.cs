using System;
using Framework.Patterns.Messaging;

namespace Identity.Domain.Events
{
    public sealed class UserCreatedEvent : Event
    {
        public UserCreatedEvent(
            Guid aggregateId,
            Guid? causedById)
            : base(
                aggregateId,
                causedById)
        {
        }
    }
}