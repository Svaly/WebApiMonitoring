using Framework.Monitoring.Logs.Factory;
using Framework.Monitoring.Logs.Logger;
using Framework.Monitoring.Logs.Publisher;
using Framework.Monitoring.Logs.Types;
using Framework.Monitoring.WebApi.Extensions;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Framework.Monitoring.WebApi
{
    public class LoggingWebApiRequestDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (new ExecutionScope(request))
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                CommitLogs(CreateLog(request, response, stopwatch.ElapsedMilliseconds));
                return response;
            }
        }

        public WebRequestProcessingLog CreateLog(HttpRequestMessage request, HttpResponseMessage response, long executionTime)
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

        private void CommitLogs(ILog log)
        {
            var logsPublisher = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogsPublisher)) as ILogsPublisher;
            var logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger)) as ILogger;

            logger.Log(log);
            logsPublisher.CommitLogsAsync();
        }
    }
}