using Framework.Service.Cqrs;
using SimpleInjector;
using System;

namespace WebApi.Config.Inject
{
    public static class RegisterApplicationServices
    {
        public static void Register(Container container)
        {
            container.Register(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Transient);
        }
    }
}