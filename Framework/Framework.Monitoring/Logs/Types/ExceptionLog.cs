using System;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class ExceptionLog : Log
    {
        public ExceptionLog(
            LogLevel logLevel,
            Exception exception)
            : base(logLevel)
        {
            Exception = exception;
            StackTrace = exception.StackTrace;
            InnerException = exception.InnerException;
            Message = exception.Message;
        }

        public string StackTrace { get; }

        public Exception InnerException { get; }

        public Exception Exception { get; }

        public string Message { get; }
    }
}