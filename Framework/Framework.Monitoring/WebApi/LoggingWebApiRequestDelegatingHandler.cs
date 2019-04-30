using Framework.Monitoring.Logs.Logger;
using Framework.Monitoring.Logs.Types;
using Framework.Monitoring.WebApi.Extensions;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Framework.Monitoring.Logs.Publisher;

namespace Framework.Monitoring.WebApi
{
    public class LoggingWebApiRequestDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var scope = new ExecutionScope(request))
            {
                var stopwatch = new Stopwatch();
                var logger = scope.ServiceLocatorGetService<ILogger>() as ILogger;
                var logsPublisher = scope.ServiceLocatorGetService<ILogsPublisher>() as ILogsPublisher;

                stopwatch.Start();
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                logger.Log(CreateLog(request, response, stopwatch.ElapsedMilliseconds));

                await logsPublisher.CommitLogsAsync();
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