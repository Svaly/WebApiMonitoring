using Framework.Monitoring.Logs.Logger;
using System;

namespace Framework.Monitoring.Logs.Factory
{
    public sealed class LoggerScopeFactory
    {
        public LoggerScope CreateRequestLoggerScope(Guid requestId)
        {
            return new LoggerScope();
        }

        public LoggerScope CreateMessageQueueLoggerScope()
        {
            return new LoggerScope();
        }
    }
}
