using System;
using Framework.Messaging.Kafka;
using Framework.Messaging.Publish;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Messaging;
using SimpleInjector;

namespace WebApi.Config.Inject
{
    public static class RegisterApplicationServices
    {
        public static void Register(Container container)
        {
            var applicationServiceAssemblies =
                AppDomain.CurrentDomain.GetAssemblies(); //.Where(a => a.FullName.EndsWith("Service")).ToArray();

            container.Register(typeof(ICommandHandler<>), applicationServiceAssemblies, Lifestyle.Transient);
            container.Register(typeof(IEventHandler<>), applicationServiceAssemblies, Lifestyle.Transient);

            container.Register(typeof(IEventRoutingKeysMapping<>), applicationServiceAssemblies, Lifestyle.Scoped);
            container.Register(typeof(IIntegrationEventPublisher<>), typeof(KafkaIntegrationEventPublisher<>), Lifestyle.Scoped);
        }
    }
}