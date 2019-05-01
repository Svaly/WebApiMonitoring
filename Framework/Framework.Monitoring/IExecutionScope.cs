using System.Net.Http;
using Framework.Patterns.Messaging;

namespace Framework.Monitoring
{
    public interface IExecutionScope
    {
        void SetUpMetadata(HttpRequestMessage request);

        void SetUpMetadata(IEvent @event);
    }
}