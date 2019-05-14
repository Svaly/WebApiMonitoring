using System;
using System.Linq;
using Framework.Patterns.Ioc;
using SimpleInjector;

namespace WebApi.Config
{
    public sealed class SimpleInjectorDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;

        public SimpleInjectorDependencyResolver(Container container)
        {
            _container = container;
        }

        public T GetService<T>()
            where T : class
        {
           return _container.GetInstance<T>();
        }

        public object GetService(Type type)
        {
            return _container.GetInstance(type);
        }
    }
}