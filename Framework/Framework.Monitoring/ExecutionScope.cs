using Framework.Monitoring.Extensions;
using Framework.Patterns.Application;
using Framework.Patterns.Messaging;
using System;
using System.Net.Http;

namespace Framework.Monitoring
{
    public sealed class ExecutionScope : IExecutionScope
    {
        private readonly IExecutionScopeMetadata _executionScopeMetadata;
        private readonly IGlobalConfigurationProvider _globalConfigurationProvider;

        public ExecutionScope(
            IExecutionScopeMetadata executionScopeMetadata,
            IGlobalConfigurationProvider globalConfigurationProvider)
        {
            _executionScopeMetadata = executionScopeMetadata;
            _globalConfigurationProvider = globalConfigurationProvider;
        }

        public void SetUpMetadata(HttpRequestMessage request)
        {
            SetUpMetadata(ProcessingScope.WebRequest, request.GetRequestIdHeader(), request.GetCorrelationIdHeader());
        }

        public void SetUpMetadata(IEvent @event)
        {
            SetUpMetadata(ProcessingScope.Event, @event.Id, @event.CorrelationId);
        }

        private void SetUpMetadata(ProcessingScope processingScope, Guid causationId, Guid correlationId)
        {
            _executionScopeMetadata.CausationId = causationId;
            _executionScopeMetadata.CorrelationId = correlationId;
            _executionScopeMetadata.ProcessingScope = processingScope;
            _executionScopeMetadata.ApplicationName = _globalConfigurationProvider.Configuration.ApplicationName;
        }
    }
}