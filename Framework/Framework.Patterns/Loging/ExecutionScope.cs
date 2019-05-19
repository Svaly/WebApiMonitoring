using Framework.Patterns.Application;
using Framework.Patterns.Messaging;
using System;
using System.Collections.Generic;

namespace Framework.Patterns.Loging
{
    public sealed class ExecutionScope : IExecutionScope
    {
        private readonly Stack<ExecutionScopeMetadata> _scopeStack;
        private readonly IGlobalConfigurationProvider _globalConfigurationProvider;

        public ExecutionScope(IGlobalConfigurationProvider globalConfigurationProvider)
        {
            _globalConfigurationProvider = globalConfigurationProvider;
            _scopeStack = new Stack<ExecutionScopeMetadata>();
        }

        public ExecutionScopeMetadata CurrentScopeMetadata
        {
            get
            {
                if (_scopeStack.Count == 0)
                {
                    return null;
                }

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
            var executionScopeMetadata = new ExecutionScopeMetadata(_globalConfigurationProvider.Configuration.ApplicationName, causationId, correlationId, processingScope);
            _scopeStack.Push(executionScopeMetadata);
        }
    }
}