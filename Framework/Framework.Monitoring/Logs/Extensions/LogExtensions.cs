using Framework.Monitoring.Logs.Types;
using Newtonsoft.Json;

namespace Framework.Monitoring.Logs.Extensions
{
    public static class LogExtensions
    {
        public static string ToJson(this ILog log)
        {
            return JsonConvert.SerializeObject(log);
        }

        public static LogType GetLogType(this ILog log)
        {
            switch (log)
            {
                case DomainEventLog p:
                    return LogType.DomainEvent;

                case ExceptionLog p:
                    return LogType.Exception;

                case WebRequestProcessingLog p:
                    return LogType.WebRequestProcessingLog;

                default:
                    return LogType.Information;
            }
        }
    }
}