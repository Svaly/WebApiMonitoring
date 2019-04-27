using System;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Logger
{
    public sealed class LoggerScope
    {
        public LoggerScope()
        {
            
        }

        public Guid CorrelationId { get; }

        public string ApplicationName { get; }

        public ProcessingScope ProcessingScope { get; }
    }
}
