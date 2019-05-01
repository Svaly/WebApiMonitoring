namespace Framework.Patterns.Messaging
{
    public interface IEventHandler<T>
        where T : IEvent
    {
        void Handle(T @event);
    }
}