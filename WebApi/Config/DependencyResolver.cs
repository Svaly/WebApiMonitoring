using System;
using System.Web.Http;
using Framework.Service.Cqrs;

namespace WebApi.Config
{
    public class DependencyResolver : IDependencyResolver
    {
        public object GetService(Type type)
        {
           return GlobalConfiguration.Configuration.DependencyResolver.GetService(type);
        }
    }
}