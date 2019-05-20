using Framework.Patterns.Loging;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Framework.Monitoring
{
    public sealed class MonitoringLogsPublisher : IMonitoringLogsPublisher
    {
        private readonly ILogsPublisher _logger;
        private readonly IExecutionScope _executionScope;

        public MonitoringLogsPublisher(IExecutionScope executionScope, ILogsPublisher logger)
        {
            _executionScope = executionScope;
            _logger = logger;
        }

        public void Publish(ILog log)
        {
            EnrichLog(log);
            _logger.Publish(log);

            Debug.WriteLine(JsonConvert.SerializeObject(log));
        }

        private void EnrichLog(ILog log)
        {
            log.CausationId = _executionScope.CurrentScopeMetadata.CausationId;
            log.CorrelationId = _executionScope.CurrentScopeMetadata.CorrelationId;
            log.ProcessingScope = _executionScope.CurrentScopeMetadata.ProcessingScope.Value;
            log.ApplicationName = _executionScope.CurrentScopeMetadata.ApplicationName;
        }
    }
}