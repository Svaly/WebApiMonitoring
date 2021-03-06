﻿using Framework.Patterns.Loging;

namespace Framework.Patterns.Monitoring
{
    public sealed class MonitoringLogsPublisher : IMonitoringLogsPublisher
    {
        private readonly IExecutionScope _executionScope;
        private readonly ILogsPublisher _logger;

        public MonitoringLogsPublisher(IExecutionScope executionScope, ILogsPublisher logger)
        {
            _executionScope = executionScope;
            _logger = logger;
        }

        public void Publish(ILog log)
        {
            EnrichLog(log);
            _logger.Publish(log);
        }

        private void EnrichLog(ILog log)
        {
            if (_executionScope.CurrentScopeMetadata == null) return;

            log.CausationId = _executionScope.CurrentScopeMetadata.CausationId;
            log.CorrelationId = _executionScope.CurrentScopeMetadata.CorrelationId;
            log.ProcessingScope = _executionScope.CurrentScopeMetadata.ProcessingScope.Value;
            log.ApplicationName = _executionScope.CurrentScopeMetadata.ApplicationName;
        }
    }
}