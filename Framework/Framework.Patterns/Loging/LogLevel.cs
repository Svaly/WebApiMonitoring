using System;
using System.Linq;

namespace Framework.Patterns.Loging
{
    public class LogLevel
    {
        private static readonly string _info = "Info";
        private static readonly string _warn = "Warn";
        private static readonly string _error = "Error";
        private static readonly string _debug = "Debug";

        private readonly string[] _availableLogLevels =
        {
            _info, _warn, _error, _debug
        };

        public LogLevel(string logLevel)
        {
            Validate(logLevel);

            Value = logLevel;
        }

        public static LogLevel Info => new LogLevel(_info);

        public static LogLevel Warn => new LogLevel(_warn);

        public static LogLevel Error => new LogLevel(_error);

        public static LogLevel Debug => new LogLevel(_debug);

        public string Value { get; }

        private void Validate(string logLevel)
        {
            if (string.IsNullOrEmpty(logLevel)) throw new ArgumentNullException(logLevel, "Log level cannot be null");

            if (!_availableLogLevels.Contains(logLevel)) throw new ArgumentException($"Invalid log level: {logLevel}");
        }
    }
}