using System.Net.Http;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Factory
{
    public interface IWebRequestProcessingLogFactory
    {
        void StartTimer();

        WebRequestProcessingLog GetLog(HttpRequestMessage request, HttpResponseMessage response);
    }
}