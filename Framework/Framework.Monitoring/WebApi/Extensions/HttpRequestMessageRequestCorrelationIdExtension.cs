using System;
using System.Linq;
using System.Net.Http;

namespace Framework.Monitoring.WebApi.Extensions
{
    public static class HttpRequestMessageRequestCorrelationIdExtension
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";

        public static Guid GetCorrelationIdHeader(this HttpRequestMessage request)
        {
            request.Headers.TryGetValues(CorrelationIdHeader, out var results);
            return results.Select(c => new Guid(c)).FirstOrDefault();
        }

        public static void AddCorrelationIdHeader(this HttpRequestMessage request, Guid id)
        {
            if (!request.Headers.Contains(CorrelationIdHeader))
            {
                request.Headers.Add(CorrelationIdHeader, id.ToString());
            }
        }
    }
}
