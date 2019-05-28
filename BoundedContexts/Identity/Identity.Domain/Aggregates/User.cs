using System;
using Framework.Patterns;
using Identity.Domain.Events;

namespace Identity.Domain.Aggregates
{
    public sealed class User : AggregateRoot
    {
        public User(Guid id, Guid? userId)
        {
            var @event = new UserCreatedEvent(id, userId);
            Enqueue(@event);
        }
    }
}