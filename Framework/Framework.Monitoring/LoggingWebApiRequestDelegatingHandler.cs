using Framework.Logs.Logger;
using Framework.Monitoring.Extensions;
using Framework.Patterns.Loging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Monitoring
{
    public class LoggingWebApiRequestDelegatingHandler : DelegatingHandler
    {
        private readonly IExecutionScope _executionScope;
        private readonly IMonitoringLogger _logger;
        private readonly ILogsPublisher _logsPublisher;
        private readonly Stopwatch _stopwatch;

        public LoggingWebApiRequestDelegatingHandler(ILogsPublisher logsPublisher, IMonitoringLogger logger, IExecutionScope executionScope)
        {
            _logsPublisher = logsPublisher;
            _logger = logger;
            _executionScope = executionScope;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
                _executionScope.SetUpMetadata(request);
                var response = await base.SendAsync(request, cancellationToken);
                _logger.Log(CreateLog(request, response));
                _logsPublisher.CommitLogsAsync();
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