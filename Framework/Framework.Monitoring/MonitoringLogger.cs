using System.Diagnostics;
using Framework.Patterns.Loging;
using Newtonsoft.Json;

namespace Framework.Monitoring
{
    public sealed class MonitoringLogger : IMonitoringLogger
    {
        private readonly ILogger _logger;
        private readonly IExecutionScope _executionScope;

        public MonitoringLogger(IExecutionScope executionScope, ILogger logger)
        {
            _executionScope = executionScope;
            _logger = logger;
        }

        public void Log(ILog log)
        {
            EnrichLog(log);
            _logger.Log(log);

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