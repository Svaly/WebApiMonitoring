namespace Framework.Messaging.Converters
{
    public interface IObjectDeserializer
    {
        T Deserialize<T>(string message);

        T Deserialize<T>(byte[] messageBytes);

        string Deserialize(byte[] bytes);
    }
}