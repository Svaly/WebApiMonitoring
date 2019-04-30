using System;

namespace Framework.Monitoring
{
    public interface IExecutionScopeMetadata
    {
       string ApplicationName { get; set; }

        Guid CausationId { get; set; }

        Guid CorrelationId { get; set; }

        ProcessingScope ProcessingScope { get; set; }
    }
}