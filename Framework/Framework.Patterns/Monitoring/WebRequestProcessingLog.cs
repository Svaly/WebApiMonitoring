using System;
using System.Net;
using Framework.Patterns.Loging;

namespace Framework.Patterns.Monitoring
{
    public class WebRequestProcessingLog : Log
    {
        public WebRequestProcessingLog(
            Guid requestId,
            string host,
            string requestMethod,
            string requestUri,
            HttpStatusCode responseStatusCode,
            long processingTime,
            LogLevel logLevel)
            : base(logLevel)
        {
            RequestMethod = requestMethod;
            RequestUri = requestUri;
            ResponseStatusCode = responseStatusCode;
            ProcessingTime = processingTime;
            RequestId = requestId;
            Host = host;
        }

        public Guid RequestId { get; }

        public string Host { get; }

        public string RequestUri { get; }

        public string RequestMethod { get; }

        public HttpStatusCode ResponseStatusCode { get; }

        public long ProcessingTime { get; }
    }
}