using Framework.Patterns.Messaging;

namespace Framework.Messaging.Consume
{
    public interface IEventRoutingEventsMapping
    {
        IEvent GetEvent(string routingKey, string message);
    }
}
