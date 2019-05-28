using System.Diagnostics;
using Framework.Patterns.Messaging;
using Identity.Domain.Events;

namespace Identity.Handlers.Event.External
{
    public sealed class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        public void Handle(UserCreatedEvent @event)
        {
            Debug.WriteLine("User Created Event Handled");
            //   throw new System.NotImplementedException("UserCreatedEvent");
        }
    }
}