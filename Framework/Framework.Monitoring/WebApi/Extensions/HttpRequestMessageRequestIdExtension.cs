using System;
using System.Linq;
using System.Net.Http;

namespace Framework.Monitoring.WebApi.Extensions
{
    public static class HttpRequestMessageRequestIdExtension
    {
        private const string RequestIdHeader = "X-Request-ID";

        public static Guid GetRequestIdHeader(this HttpRequestMessage request)
        {
           request.Headers.TryGetValues(RequestIdHeader, out var results);
           return results.Select(c => new Guid(c)).FirstOrDefault();       
        }

        public static void AddRequestIdHeader(this HttpRequestMessage request, Guid id)
        {
            if (!request.Headers.Contains(RequestIdHeader))
            {
                request.Headers.Add(RequestIdHeader, id.ToString());
            }
        }
    }
}
