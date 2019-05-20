namespace Framework.Patterns.Loging
{
    public interface ILogsPublisher
    {
        void Publish(ILog log);
    }
}