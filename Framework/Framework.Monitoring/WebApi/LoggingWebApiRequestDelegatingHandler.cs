using Framework.Monitoring.Logs.Logger;
using Framework.Monitoring.Logs.Types;
using Framework.Monitoring.WebApi.Extensions;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Framework.Monitoring.WebApi
{
    public class LoggingWebApiRequestDelegatingHandler : DelegatingHandler
    {
        private ILogger _logger;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (GlobalConfiguration.Configuration.DependencyResolver.BeginScope())
            {
                var stopwatch = StartStopwatch();
                _logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger)) as ILogger;

                AddCorrelationIdHeadersToRequest(request);
                var requestMetadataLog = BuildRequestMetadataLog(request);
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                AddResponseMetadataToLog(ref requestMetadataLog, response, stopwatch.ElapsedMilliseconds);

                await CommitLogs(requestMetadataLog);
                return response;
            }
        }

        private RequestMetadataLog BuildRequestMetadataLog(HttpRequestMessage request)
        {
            var requestMetadataLog = new RequestMetadataLog(
                request.GetCorrelationIdHeader(),
                request.GetRequestIdHeader(),
                request.GetType(),
                request.Headers.Host,
                "",
                string.Empty,
                request.Method.Method,
                request.RequestUri.ToString());

            return requestMetadataLog;
        }

        private void AddResponseMetadataToLog(ref RequestMetadataLog logMetadata, HttpResponseMessage response, long elapsedMilliseconds)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ProcessingTime = elapsedMilliseconds;
        }

        private Task CommitLogs(ILog log)
        {
            _logger.EnqueueLog(log);
            return _logger.CommitLogsAsync();
        }

        private void AddCorrelationIdHeadersToRequest(HttpRequestMessage request)
        {
            var requestId = Guid.NewGuid();
            request.AddRequestIdHeader(requestId);
            request.AddCorrelationIdHeader(requestId);
        }

        private Stopwatch StartStopwatch()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }
    }
}
