using System;
using System.Linq;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class LogType
    {
        private static readonly string _domainEvent = "DomainEvent";
        private static readonly string _information = "Information";
        private static readonly string _exception = "Exception";
        private static readonly string _requestMetadata = "RequestMetadata";
        private static readonly string _commandMetadata = "CommandMetadata";

        private static readonly string[] AvailableLogTypes =
        {
            _domainEvent, _information, _exception, _requestMetadata, _commandMetadata
        };

        public LogType(string logType)
        {
            Validate(logType);

            Value = logType;
        }

        public static LogType Information = new LogType(_information);

        public static LogType Exception = new LogType(_exception);

        public static LogType RequestMetadata = new LogType(_requestMetadata);

        public static LogType CommandMetadata = new LogType(_commandMetadata);

        public static LogType DomainEvent => new LogType(_domainEvent);

        public string Value { get; }

        private void Validate(string logType)
        {
            if (string.IsNullOrEmpty(logType)) throw new ArgumentNullException(logType, "Log type cannot be null");

            if (!AvailableLogTypes.Contains(logType)) throw new ArgumentException($"Invalid log type: {logType}");
        }
    }
}