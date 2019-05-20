using Framework.Monitoring;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebApi.App_Data;
using WebApi.Config;
using WebApi.Config.Inject;
using WebApi.Messaging;

namespace WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<LoggingWebApiRequestDelegatingHandler>(Lifestyle.Scoped);

            RegisterFramework.Register(container);
            RegisterApplicationServices.Register(container);
            RegisterMessageQueue.Register(container);

            // This is an extension method from the simple injector web api integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<LoggingWebApiRequestDelegatingHandler>(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container, DependencyResolverScopeOption.UseAmbientScope);

            KafkaMessageQueueConsumersRegistrar.RegisterConsumeConnection<KafkaLogsMessagesProcessor>(container, "ServiceMonitoringLogsConsume");
            KafkaMessageQueueConsumersRegistrar.RegisterConsumeConnection<KafkaConsumedIntegrationMessageProcessor>(container, "ServiceMonitoringDomainEventsConsume");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
