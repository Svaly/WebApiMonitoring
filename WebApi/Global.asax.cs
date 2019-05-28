using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Framework.Messaging.Kafka.Consume;
using Framework.WebApi;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
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

            container.Register<MonitoringWebApiRequestDelegatingHandler>(Lifestyle.Scoped);
            container.Register<IntegrationEventsPublishDelegatingHandler>(Lifestyle.Scoped);
            container.Register<LogsPublishDelegatingHandler>(Lifestyle.Scoped);

            RegisterFramework.Register(container);
            RegisterApplicationServices.Register(container);
            RegisterMessageQueue.Register(container);

            // This is an extension method from the simple injector web api integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<LogsPublishDelegatingHandler>(container));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<MonitoringWebApiRequestDelegatingHandler>(container));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<IntegrationEventsPublishDelegatingHandler>(container));

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(
                container,
                DependencyResolverScopeOption.UseAmbientScope);

            KafkaMessageQueueConsumersRegistrar.RegisterConsumeConnection<KafkaLogsMessagesProcessor>(
                container,
                "ServiceMonitoringLogsConsume");

            KafkaMessageQueueConsumersRegistrar.RegisterConsumeConnection<IKafkaConsumedMessageProcessor>(
                container,
                "ServiceMonitoringDomainEventsConsume");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}