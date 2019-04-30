using Framework.Patterns.Messaging;

namespace Framework.Messaging.Consume
{
    public interface IEventRoutingEventsMapping
    {
        Event GetEvent(string routingKey, string message);
    }
}
