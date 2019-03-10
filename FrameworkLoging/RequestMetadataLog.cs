using System;
using System.Net;

namespace Framework.Loging
{
    public class RequestMetadataLog : Log
    {
        public RequestMetadataLog(Guid correlationId, Guid requestId, Type type, string host, string applicationName, string data, string requestMethod, string requestUri) 
            : base(correlationId, requestId, type, applicationName, data)
        {
            RequestMethod = requestMethod;
            RequestUri = requestUri;
            RequestId = requestId;
            Host = host;
            RequestTimestamp = DateTime.UtcNow;
        }

        public Guid RequestId { get; }

        public string Host { get; }

        public string RequestUri { get; }

        public string RequestMethod { get;}

        public DateTime? RequestTimestamp { get; }

        public HttpStatusCode ResponseStatusCode { get; set; }

        public long ProcessingTime { get; set; }
    }
}
