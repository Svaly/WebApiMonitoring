using System;
using System.Linq;
using System.Net.Http;

namespace Framework.WebApi.Extensions
{
    public static class HttpRequestMessageRequestCorrelationIdExtension
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";

        public static Guid GetCorrelationIdHeader(this HttpRequestMessage request)
        {
            request.Headers.TryGetValues(CorrelationIdHeader, out var correlationIdHeaders);
            var correlationIdHeader = correlationIdHeaders?.SingleOrDefault();

            if (!Guid.TryParse(correlationIdHeader, out var id))
            {
                id = Guid.NewGuid();
                request.Headers.Add(CorrelationIdHeader, id.ToString());
            }

            return id;
        }
    }
}