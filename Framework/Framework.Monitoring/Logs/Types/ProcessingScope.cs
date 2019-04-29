using System;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class ProcessingScope : IProcessingScope
    {
        public Guid CorrelationId { get; private set; }

        public string ApplicationName { get; private set; }

        public ProcessingScopeType ProcessingScopeType { get; private set; }

        public void SetScope(Guid correlationId, string applicationName, ProcessingScopeType processingScopeType)
        {
            CorrelationId = correlationId;
            ApplicationName = applicationName;
            ProcessingScopeType = processingScopeType;
        }
    }
}