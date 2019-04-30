using System;

namespace Framework.Patterns.Cqrs
{
    public interface IDependencyResolver
    {
        object GetService(Type type);
    }
}
