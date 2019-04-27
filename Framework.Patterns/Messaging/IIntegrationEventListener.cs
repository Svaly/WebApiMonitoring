namespace Framework.Patterns.Messaging
{
    public interface IIntegrationEventListener
    {
        void ListenToAllEnabledConnections();
    }
}