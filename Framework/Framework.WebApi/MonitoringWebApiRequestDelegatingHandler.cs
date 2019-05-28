using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Framework.Monitoring.Extensions;
using Framework.Patterns.Loging;
using Framework.Patterns.Messaging;

namespace Framework.Monitoring
{
    public class MonitoringWebApiRequestDelegatingHandler : DelegatingHandler
    {
        private readonly IExecutionScope _executionScope;
        private readonly IIntegrationEventsProcessor _integrationEventsProcessor;
        private readonly ILogsProcessor _logsProcessor;
        private readonly IMonitoringLogsPublisher _logsPublisher;
        private readonly Stopwatch _stopwatch;

        public MonitoringWebApiRequestDelegatingHandler(
            ILogsProcessor logsProcessor,
            IMonitoringLogsPublisher logsPublisher,
            IExecutionScope executionScope,
            IIntegrationEventsProcessor integrationEventsProcessor)
        {
            _logsPublisher = logsPublisher;
            _logsProcessor = logsProcessor;
            _executionScope = executionScope;
            _integrationEventsProcessor = integrationEventsProcessor;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            StartExecutionScope(request);

            var response = await base.SendAsync(request, cancellationToken);
            await SendIntegrationEventsAsync();
            await SendLogsAsync(request, response);

            StopExecutionScope();
            return response;
        }

        private void StartExecutionScope(HttpRequestMessage request)
        {
            _executionScope.StartScope(ProcessingScope.WebRequest, request.GetRequestIdHeader(), request.GetCorrelationIdHeader());
        }

        private void StopExecutionScope()
        {
            _executionScope.UnwindScope();
        }

        private async Task SendIntegrationEventsAsync()
        {
            await _integrationEventsProcessor.ProcessAsync();
        }

        private async Task SendLogsAsync(HttpRequestMessage request, HttpResponseMessage response)
        {
            _logsPublisher.Publish(CreateLog(request, response));
            await _logsProcessor.ProcessAsync();
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