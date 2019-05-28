using System;
using System.Linq;

namespace Framework.Patterns.Loging
{
    public sealed class LogType
    {
        private static readonly string _domainEvent = "DomainEvent";
        private static readonly string _information = "Information";
        private static readonly string _exception = "Exception";
        private static readonly string _webRequestProcessingLog = "WebRequestProcessingLog";
        private static readonly string _messagingEventProcessingLog = "MessagingEventProcessingLog";

        private static readonly string[] AvailableLogTypes =
        {
            _domainEvent, _information, _exception, _webRequestProcessingLog, _messagingEventProcessingLog
        };

        public static LogType MessagingEventProcessingLog = new LogType(_messagingEventProcessingLog);

        public static LogType Information = new LogType(_information);

        public static LogType Exception = new LogType(_exception);

        public static LogType WebRequestProcessingLog = new LogType(_webRequestProcessingLog);

        public LogType(string logType)
        {
            Validate(logType);

            Value = logType;
        }

        public static LogType DomainEvent => new LogType(_domainEvent);

        public string Value { get; }

        private void Validate(string logType)
        {
            if (string.IsNullOrEmpty(logType)) throw new ArgumentNullException(logType, "Log type cannot be null");

            if (!AvailableLogTypes.Contains(logType)) throw new ArgumentException($"Invalid log type: {logType}");
        }
    }
}