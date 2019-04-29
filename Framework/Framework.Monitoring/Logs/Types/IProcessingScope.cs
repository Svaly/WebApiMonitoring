using System;

namespace Framework.Monitoring.Logs.Types
{
    public interface IProcessingScope
    {
        Guid CorrelationId { get; }

        string ApplicationName { get; }

        ProcessingScopeType ProcessingScopeType { get; }

        void SetScope(Guid correlationId, string applicationName, ProcessingScopeType processingScopeType);
    }
}