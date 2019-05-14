using System;

namespace Framework.Patterns.Ioc
{
    public interface IDependencyResolver
    {
        T GetService<T>()
            where T : class;

        object GetService(Type type);
    }
}
