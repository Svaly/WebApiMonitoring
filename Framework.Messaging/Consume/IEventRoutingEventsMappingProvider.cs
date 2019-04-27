namespace Framework.Messaging.Consume
{
    public interface IEventRoutingEventsMappingProvider
    {
        IEventRoutingEventsMapping GetMapping(string connectionName);
    }
}