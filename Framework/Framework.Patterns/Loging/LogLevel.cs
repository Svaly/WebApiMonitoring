﻿using System;
using System.Linq;

namespace Framework.Patterns.Loging
{
    public class LogLevel
    {
        private static string _info = "Info";
        private static string _warn = "Warn";
        private static string _error = "Error";
        private static string _debug = "Debug";

        public static LogLevel Info => new LogLevel(_info);

        public static LogLevel Warn => new LogLevel(_warn);

        public static LogLevel Error => new LogLevel(_error);

        public static LogLevel Debug => new LogLevel(_debug);

        private readonly string[] _availableLogLevels =
        {
            _info, _warn, _error, _debug
        };

        public LogLevel(string logLevel)
        {
            Validate(logLevel);

            Value = logLevel;
        }

        public string Value { get; }

        private void Validate(string logLevel)
        {
            if (string.IsNullOrEmpty(logLevel)) throw new ArgumentNullException(logLevel, "Log level cannot be null");

            if (!_availableLogLevels.Contains(logLevel)) throw new ArgumentException($"Invalid log level: {logLevel}");
        }
    }
}