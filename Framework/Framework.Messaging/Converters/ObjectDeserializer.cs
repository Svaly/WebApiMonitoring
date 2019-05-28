using System.Text;
using Framework.Patterns.Validation;
using Newtonsoft.Json;

namespace Framework.Messaging.Converters
{
    public sealed class ObjectDeserializer : IObjectDeserializer
    {
        public T Deserialize<T>(string message)
        {
            Guard.NotNull(() => message, message);

            return JsonConvert.DeserializeObject<T>(message);
        }

        public T Deserialize<T>(byte[] messageBytes)
        {
            Guard.NotNull(() => messageBytes, messageBytes);

            var message = Deserialize(messageBytes);
            return JsonConvert.DeserializeObject<T>(message);
        }

        public string Deserialize(byte[] bytes)
        {
            Guard.NotNull(() => bytes, bytes);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}