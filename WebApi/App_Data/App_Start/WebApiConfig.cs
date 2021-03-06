﻿using System.Web.Http;
using Swashbuckle.Application;

namespace WebApi.App_Data
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableSwagger(c => c.SingleApiVersion("v1", "WebApi")).EnableSwaggerUi();

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}