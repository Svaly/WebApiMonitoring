using System;
using Framework.Patterns.Messaging;

namespace Domain.Contracts.IntegrationEvents.Identity
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