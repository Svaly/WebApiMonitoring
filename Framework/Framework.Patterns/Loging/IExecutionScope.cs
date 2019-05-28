using System;
using Framework.Patterns.Messaging;
using Framework.Patterns.Monitoring;

namespace Framework.Patterns.Loging
{
    public interface IExecutionScope
    {
        ExecutionScopeMetadata CurrentScopeMetadata { get; }

        void StartScope(ProcessingScope processingScope, Guid causationId, Guid correlationId);

        void StartScope(IEvent @event);

        void UnwindScope();
    }
}