using Framework.Patterns.Messaging;
using Newtonsoft.Json;

namespace Framework.Patterns.Extension
{
    public static class EventExtension
    {
        public static string ToJson(this IEvent @event)
        {
            return JsonConvert.SerializeObject(@event);
        }
    }
}
