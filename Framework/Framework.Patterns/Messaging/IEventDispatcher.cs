namespace Framework.Patterns.Messaging
{
    public interface IEventDispatcher
    {
        void Dispatch(IEvent @event);
    }
}