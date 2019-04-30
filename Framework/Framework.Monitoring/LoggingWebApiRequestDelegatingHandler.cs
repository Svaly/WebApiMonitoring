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
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var scope = new ExecutionScope(request))
            {
                var stopwatch = new Stopwatch();
                var logger = scope.ServiceLocatorGetService<IMonitoringLogger>() as ILogger;
                var logsPublisher = scope.ServiceLocatorGetService<ILogsPublisher>() as ILogsPublisher;

                stopwatch.Start();
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                logger.Log(CreateLog(request, response, stopwatch.ElapsedMilliseconds));

                logsPublisher.CommitLogsAsync();
                return response;
            }
        }

        private WebRequestProcessingLog CreateLog(HttpRequestMessage request, HttpResponseMessage response, long executionTime)
        {
            return new WebRequestProcessingLog(
                request.GetRequestIdHeader(),
                request.Headers.Host,
                request.Method.Method,
                request.RequestUri.ToString(),
                response.StatusCode,
                executionTime,
                LogLevel.Info);
        }
    }
}