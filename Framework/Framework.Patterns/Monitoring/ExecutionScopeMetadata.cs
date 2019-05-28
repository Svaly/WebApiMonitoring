using System;
using Framework.Patterns.Loging;

namespace Framework.Patterns.Monitoring
{
    public sealed class ExecutionScopeMetadata
    {
        public ExecutionScopeMetadata(string applicationName, Guid causationId, Guid correlationId, ProcessingScope processingScope)
        {
            ApplicationName = applicationName;
            CausationId = causationId;
            CorrelationId = correlationId;
            ProcessingScope = processingScope;
        }

        public string ApplicationName { get; }

        public Guid CausationId { get; }

        public Guid CorrelationId { get; }

        public ProcessingScope ProcessingScope { get; }
    }
}