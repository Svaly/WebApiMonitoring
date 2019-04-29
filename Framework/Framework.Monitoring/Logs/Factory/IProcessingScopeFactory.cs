using System.Net.Http;
using Framework.Monitoring.Logs.Types;
using Framework.Patterns.Messaging;

namespace Framework.Monitoring.Logs.Factory
{
    public interface IProcessingScopeFactory
    {
        IProcessingScope CreateWebRequestLoggerScope(HttpRequestMessage request);

        IProcessingScope CreateMessageQueueLoggerScope(IEvent @event);
    }
}