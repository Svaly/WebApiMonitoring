using Framework.Monitoring.Logs.Types;
using Framework.Monitoring.WebApi.Extensions;
using System.Diagnostics;
using System.Net.Http;

namespace Framework.Monitoring.Logs.Factory
{
    public sealed class WebRequestProcessingLogFactory : IWebRequestProcessingLogFactory
    {
        private readonly IProcessingScope _processingScope;
        private readonly Stopwatch _stopwatch;

        public WebRequestProcessingLogFactory(IProcessingScope processingScope)
        {
            _processingScope = processingScope;
            _stopwatch = new Stopwatch();
        }

        public void StartTimer()
        {
            _stopwatch.Start();
        }

        public WebRequestProcessingLog GetLog(HttpRequestMessage request, HttpResponseMessage response)
        {
            _stopwatch.Stop();

            return new WebRequestProcessingLog(
                request.GetRequestIdHeader(),
                request.Headers.Host,
                request.Method.Method,
                request.RequestUri.ToString(),
                response.StatusCode,
                _stopwatch.ElapsedMilliseconds,
                LogLevel.Info);
        }
    }
}
