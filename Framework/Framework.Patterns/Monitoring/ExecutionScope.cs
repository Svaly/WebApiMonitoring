using System;
using System.Collections.Generic;
using Framework.Patterns.Application;
using Framework.Patterns.Loging;
using Framework.Patterns.Messaging;

namespace Framework.Patterns.Monitoring
{
    public sealed class ExecutionScope : IExecutionScope
    {
        private readonly IGlobalConfigurationProvider _globalConfigurationProvider;
        private readonly Stack<ExecutionScopeMetadata> _scopeStack;

        public ExecutionScope(IGlobalConfigurationProvider globalConfigurationProvider)
        {
            _globalConfigurationProvider = globalConfigurationProvider;
            _scopeStack = new Stack<ExecutionScopeMetadata>();
        }

        public ExecutionScopeMetadata CurrentScopeMetadata
        {
            get
            {
                if (_scopeStack.Count == 0) return null;

                return _scopeStack.Peek();
            }
        }

        public void StartScope(ProcessingScope processingScope, Guid causationId, Guid correlationId)
        {
            SetUpScopeMetadata(ProcessingScope.WebRequest, causationId, correlationId);
        }

        public void StartScope(IEvent @event)
        {
            SetUpScopeMetadata(ProcessingScope.Event, @event.EventId, @event.CorrelationId);
        }

        public void UnwindScope()
        {
            _scopeStack.Pop();
        }

        private void SetUpScopeMetadata(ProcessingScope processingScope, Guid causationId, Guid correlationId)
        {
            var executionScopeMetadata = new ExecutionScopeMetadata(
                _globalConfigurationProvider.Configuration.ApplicationName,
                causationId,
                correlationId,
                processingScope);
            _scopeStack.Push(executionScopeMetadata);
        }
    }
}