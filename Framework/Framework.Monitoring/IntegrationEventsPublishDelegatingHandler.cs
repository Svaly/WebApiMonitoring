using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Framework.Patterns.Messaging;

namespace Framework.WebApi
{
    public class IntegrationEventsPublishDelegatingHandler : DelegatingHandler
    {
        private readonly IIntegrationEventsProcessor _integrationEventsProcessor;

        public IntegrationEventsPublishDelegatingHandler(IIntegrationEventsProcessor integrationEventsProcessor)
        {
            _integrationEventsProcessor = integrationEventsProcessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            await _integrationEventsProcessor.ProcessAsync();

            return response;
        }
    }
}