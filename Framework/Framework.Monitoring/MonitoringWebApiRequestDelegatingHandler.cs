using Framework.Monitoring.Extensions;
using Framework.Patterns.Loging;
using Framework.Patterns.Messaging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Monitoring
{
    public class MonitoringWebApiRequestDelegatingHandler : DelegatingHandler
    {
        private readonly IExecutionScope _executionScope;
        private readonly IMonitoringLogsPublisher _logsPublisher;
        private readonly ILogsProcessor _logsProcessor;
        private readonly IIntegrationEventsProcessor _integrationEventsProcessor;
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
            _executionScope.StartScope(ProcessingScope.WebRequest, request.GetRequestIdHeader(), request.GetCorrelationIdHeader());

            var response = await base.SendAsync(request, cancellationToken);
            _logsPublisher.Publish(CreateLog(request, response));

            await _integrationEventsProcessor.ProcessAsync();
            await _logsProcessor.ProcessAsync();
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