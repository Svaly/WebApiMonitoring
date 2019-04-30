using Framework.Monitoring.Logs.Publisher;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public sealed class Logger : ILogger
    {
        private readonly ILogsQueue _logsQueue;
        private readonly IExecutionScopeMetadata _executionScopeMetadata;

        public Logger(ILogsQueue logsQueue, IExecutionScopeMetadata executionScopeMetadata)
        {
            _logsQueue = logsQueue;
            _executionScopeMetadata = executionScopeMetadata;
        }

        public void Log(ILog log)
        {
            EnrichLog(log);
            _logsQueue.Enqueue(log);
        }

        private void EnrichLog(ILog log)
        {
            log.CausationId = _executionScopeMetadata.CausationId;
            log.CorrelationId = _executionScopeMetadata.CorrelationId;
            log.ProcessingScope = _executionScopeMetadata.ProcessingScope.Value;
            log.ApplicationName= _executionScopeMetadata.ApplicationName;

        }
    }
}