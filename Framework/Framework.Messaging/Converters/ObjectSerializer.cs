using System.Text;
using Framework.Patterns.Validation;
using Newtonsoft.Json;

namespace Framework.Messaging.Converters
{
    public sealed class ObjectSerializer : IObjectSerializer
    {
        public string SerializeToJsonString(object @object)
        {
            Guard.NotNull(() => @object, @object);

            return JsonConvert.SerializeObject(@object);
        }

        public byte[] SerializeToJsonStringBytes(string @object)
        {
            return Encoding.UTF8.GetBytes(@object);
        }
    }
}