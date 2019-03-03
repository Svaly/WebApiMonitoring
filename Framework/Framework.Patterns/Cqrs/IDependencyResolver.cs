using System;

namespace Framework.Service.Cqrs
{
    public interface IDependencyResolver
    {
        object GetService(Type type);
    }
}
