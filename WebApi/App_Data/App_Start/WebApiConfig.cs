using System.Web.Http;
using Framework.Monitoring.WebApi;
using Swashbuckle.Application;

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
