using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public interface ILogger
    {
        void Log(ILog log);
    }
}