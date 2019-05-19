using System;
using Framework.Patterns.Messaging;

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