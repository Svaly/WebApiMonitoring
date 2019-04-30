namespace Framework.Messaging.Converters
{
    public interface IObjectSerializer
    {
        string SerializeToJsonString(object @object);

        byte[] SerializeToJsonStringBytes(string @object);
    }
}