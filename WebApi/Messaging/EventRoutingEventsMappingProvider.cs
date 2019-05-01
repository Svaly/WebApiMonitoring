using Framework.Messaging.Consume;

namespace WebApi.Messaging
{
    public class EventRoutingEventsMappingProvider : IEventRoutingEventsMappingProvider
    {
        private readonly IEventRoutingEventsMapping _mapping;

        public EventRoutingEventsMappingProvider(IEventRoutingEventsMapping mapping)
        {
            _mapping = mapping;
        }

        public IEventRoutingEventsMapping GetMapping(string connectionName)
        {
            return _mapping;
        }
    }
}