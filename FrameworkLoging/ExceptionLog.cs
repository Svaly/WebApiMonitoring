using System;

namespace Framework.Loging
{
    public sealed class ExceptionLog : Log
    {
        public ExceptionLog(Exception exception, Guid correlationId, Guid causationId, string applicationName) 
            : base(causationId, correlationId, exception.GetType(), applicationName, exception.ToString())
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