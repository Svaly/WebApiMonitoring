using Framework.Monitoring.Logs.Types;
using Framework.Monitoring.WebApi.Extensions;
using Framework.Patterns.Application;
using Framework.Patterns.Messaging;
using System.Net.Http;

namespace Framework.Monitoring.Logs.Factory
{
    public sealed class ProcessingScopeFactory : IProcessingScopeFactory
    {
        private readonly string _applicationName;
        private readonly IProcessingScope _processingScope;

        public ProcessingScopeFactory(IGlobalConfigurationProvider globalConfigurationProvider, IProcessingScope processingScope)
        {
            _processingScope = processingScope;
            _applicationName = globalConfigurationProvider.Configuration.ApplicationName;
        }

        public IProcessingScope CreateWebRequestLoggerScope(HttpRequestMessage request)
        {
            _processingScope.SetScope(request.GetCorrelationIdHeader(), _applicationName, ProcessingScopeType.WebRequest);
            return _processingScope;
        }

        public IProcessingScope CreateMessageQueueLoggerScope(IEvent @event)
        {
            _processingScope.SetScope(@event.CorrelationId, _applicationName, ProcessingScopeType.Event);
            return _processingScope;
        }
    }
}
