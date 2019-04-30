using Framework.Monitoring.Extensions;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Framework.Patterns.Application;
using Framework.Patterns.Messaging;

namespace Framework.Monitoring
{
    public sealed class ExecutionScope : IDisposable
    {
        private readonly IDependencyScope _dependencyScope;

        public ExecutionScope(HttpRequestMessage request)
            : this(ProcessingScope.WebRequest, request.GetRequestIdHeader(), request.GetCorrelationIdHeader())
        {
        }

        public ExecutionScope(IEvent @event)
            : this(ProcessingScope.Event, @event.Id, @event.CorrelationId)
        {
        }

        private ExecutionScope(ProcessingScope processingScope, Guid causationId, Guid correlationId)
        {
            _dependencyScope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            SetUpMetadata(processingScope, causationId, correlationId);
        }

        public object ServiceLocatorGetService<T>() => _dependencyScope.GetService(typeof(T));

        public void Dispose()
        {
            _dependencyScope.Dispose();
        }

        private void SetUpMetadata(ProcessingScope processingScope, Guid causationId, Guid correlationId)
        {
            var metadata = ServiceLocatorGetService<IExecutionScopeMetadata>() as IExecutionScopeMetadata;
            var globalConfig = ServiceLocatorGetService<IGlobalConfigurationProvider>() as IGlobalConfigurationProvider;
            metadata.CausationId = causationId;
            metadata.CorrelationId = correlationId;
            metadata.ProcessingScope = processingScope;
            metadata.ApplicationName = globalConfig.Configuration.ApplicationName;
        }
    }
}