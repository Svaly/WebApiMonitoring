using Framework.Monitoring.Logs.Types;
using Framework.Monitoring.WebApi.Extensions;
using Framework.Patterns.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Framework.Monitoring.Logs.Factory
{
    public sealed class ExecutionScope : IDisposable
    {
        private static Stack<ExecutionScope> ScopeStack = new Stack<ExecutionScope>();
        private readonly IDependencyScope _dependencyScope;

        public ExecutionScope(HttpRequestMessage request)
            : this(ProcessingScopeType.WebRequest, request.GetRequestIdHeader(), request.GetCorrelationIdHeader())
        {
        }

        public ExecutionScope(IEvent @event)
            : this(ProcessingScopeType.Event, @event.Id, @event.CorrelationId)
        {
        }

        private ExecutionScope(ProcessingScopeType processingScopeType, Guid causationId, Guid correlationId)
        {
            _dependencyScope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            CausationId = causationId;
            CorrelationId = correlationId;
            ProcessingScopeType = processingScopeType;

            if (Current is null)
            {
                //var globalConfigurationProvider = ServiceLocatorGetService<IGlobalConfigurationProvider>() as IGlobalConfigurationProvider;
                //ApplicationName = globalConfigurationProvider?.Configuration.ApplicationName;
                ApplicationName = "XD";
            }

            ScopeStack.Push(this);
        }

        public static ExecutionScope Current => ScopeStack.Count == 0 ? null : ScopeStack.Peek();

        public static string ApplicationName { get; private set; }

        public Guid CausationId { get; }

        public Guid CorrelationId { get; }

        public ProcessingScopeType ProcessingScopeType { get; }

        public object ServiceLocatorGetService<T>()
            where T : class => _dependencyScope.GetService(typeof(T));

        public void Dispose()
        {
            ScopeStack.Pop();
            _dependencyScope.Dispose();
        }
    }
}