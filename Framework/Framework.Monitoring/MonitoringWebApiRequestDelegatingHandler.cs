using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Framework.Patterns.Loging;
using Framework.Patterns.Monitoring;
using Framework.WebApi.Extensions;

namespace Framework.WebApi
{
    public class MonitoringWebApiRequestDelegatingHandler : DelegatingHandler
    {
        private readonly IExecutionScope _executionScope;
        private readonly IMonitoringLogsPublisher _logsPublisher;
        private readonly Stopwatch _stopwatch;

        public MonitoringWebApiRequestDelegatingHandler(
            IMonitoringLogsPublisher logsPublisher,
            IExecutionScope executionScope)
        {
            _logsPublisher = logsPublisher;
            _executionScope = executionScope;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _executionScope.StartScope(ProcessingScope.WebRequest, request.GetRequestIdHeader(), request.GetCorrelationIdHeader());
            var response = await base.SendAsync(request, cancellationToken);
            _logsPublisher.Publish(CreateLog(request, response));
            _executionScope.UnwindScope();
            return response;
        }

        private WebRequestProcessingLog CreateLog(HttpRequestMessage request, HttpResponseMessage response)
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