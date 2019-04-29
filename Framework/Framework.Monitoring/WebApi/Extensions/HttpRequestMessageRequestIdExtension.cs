using System;
using System.Linq;
using System.Net.Http;

namespace Framework.Monitoring.WebApi.Extensions
{
    public static class HttpRequestMessageRequestIdExtension
    {
        private const string RequestIdHeader = "X-WebRequest-ID";

        public static Guid GetRequestIdHeader(this HttpRequestMessage request)
        {
            request.Headers.TryGetValues(RequestIdHeader, out var requestIdHeaders);
            var requestIdHeader = requestIdHeaders?.SingleOrDefault();

            if (!Guid.TryParse(requestIdHeader, out var id))
            {
                id = Guid.NewGuid();
                request.Headers.Add(RequestIdHeader, id.ToString());
            }

            return id;
        }
    }
}
