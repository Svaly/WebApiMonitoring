using Framework.Patterns.Loging;

namespace Framework.Monitoring
{
    public sealed class MonitoringLogger : IMonitoringLogger
    {
        private readonly ILogger _logger;
        private readonly IExecutionScopeMetadata _executionScopeMetadata;

        public MonitoringLogger(IExecutionScopeMetadata executionScopeMetadata, ILogger logger)
        {
            _executionScopeMetadata = executionScopeMetadata;
            _logger = logger;
        }

        public void Log(ILog log)
        {
            EnrichLog(log);
            _logger.Log(log);
        }

        private void EnrichLog(ILog log)
        {
            log.CausationId = _executionScopeMetadata.CausationId;
            log.CorrelationId = _executionScopeMetadata.CorrelationId;
            log.ProcessingScope = _executionScopeMetadata.ProcessingScope.Value;
            log.ApplicationName = _executionScopeMetadata.ApplicationName;
        }
    }
}