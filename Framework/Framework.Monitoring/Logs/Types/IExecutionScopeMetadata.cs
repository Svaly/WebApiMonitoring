using System;

namespace Framework.Monitoring.Logs.Types
{
    public interface IExecutionScopeMetadata
    {
       string ApplicationName { get; set; }

        Guid CausationId { get; set; }

        Guid CorrelationId { get; set; }

        ProcessingScope ProcessingScope { get; set; }
    }
}