using Framework.Patterns.Messaging;
using System;

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