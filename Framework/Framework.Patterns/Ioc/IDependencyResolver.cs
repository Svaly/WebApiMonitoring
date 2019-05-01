namespace Framework.Patterns.Ioc
{
    public interface IDependencyResolver
    {
        T GetService<T>()
            where T : class;
    }
}
