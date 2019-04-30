using Framework.Monitoring;
using Swashbuckle.Application;
using System.Web.Http;

namespace WebApi.App_Data
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableSwagger(c => c.SingleApiVersion("v1", "WebApi")).EnableSwaggerUi();

            config.MessageHandlers.Add(new LoggingWebApiRequestDelegatingHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
