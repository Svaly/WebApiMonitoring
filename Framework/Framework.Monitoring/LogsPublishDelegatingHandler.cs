using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Framework.Patterns.Loging;

namespace Framework.WebApi
{
    public class LogsPublishDelegatingHandler : DelegatingHandler
    {
        private readonly ILogsProcessor _logsProcessor;

        public LogsPublishDelegatingHandler(ILogsProcessor logsProcessor)
        {
            _logsProcessor = logsProcessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            await _logsProcessor.ProcessAsync();
            return response;
        }
    }
}