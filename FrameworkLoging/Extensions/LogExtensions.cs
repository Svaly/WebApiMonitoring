using Newtonsoft.Json;

namespace Framework.Loging.Extensions
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
                    return new LogType(LogType.DomainEvent);

                case ExceptionLog p:
                    return new LogType(LogType.Exception);

                case RequestMetadataLog p:
                    return new LogType(LogType.RequestMetadata);

                default:
                    return new LogType(LogType.Information);
            }
        }
    }
}