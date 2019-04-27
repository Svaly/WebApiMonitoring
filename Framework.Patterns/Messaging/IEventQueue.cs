namespace Framework.Patterns.Messaging
{
    public interface IEventQueue
    {
        bool HasEvents { get; }

        void Enqueue(IEvent @event);

        IEvent Dequeue();
    }
}
