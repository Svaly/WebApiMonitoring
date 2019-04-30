using System;

namespace Framework.Patterns.Loging
{
    public sealed class ExceptionLog : Log
    {
        public ExceptionLog(
            LogLevel logLevel,
            Exception exception,
            string message = null)
            : base(logLevel, message)
        {
            Exception = exception;
            ExceptionMessage = exception.Message;
        }

        public string ExceptionMessage { get; }

        public Exception Exception { get; }
    }
}