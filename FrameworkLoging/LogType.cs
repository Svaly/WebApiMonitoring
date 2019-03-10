using System;
using System.Linq;

namespace Framework.Loging
{
    public sealed class LogType
    {
        public static string DomainEvent = "DomainEvent";
        public static string Information = "Information";
        public static string Exception = "Exception";
        public static string RequestMetadata = "RequestMetadata";
        public static string CommandMetadata = "CommandMetadata";

        private readonly string[] _availableLogTypes =
        {
            DomainEvent, Exception, RequestMetadata, CommandMetadata, Information
        };

        public LogType(string logType)
        {
            Validate(logType);

            Value = logType;
        }

        public string Value { get; }

        private void Validate(string logType)
        {
            if (string.IsNullOrEmpty(logType))
            {
                throw new ArgumentNullException(logType, "Log type cannot be null");
            }

            if (!_availableLogTypes.Contains(logType))
            {
                throw new ArgumentException($"Invalid log type: {logType}");
            }
        }
    }
}