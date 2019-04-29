namespace Framework.Patterns.Application
{
    public interface IGlobalConfigurationProvider
    {
        GlobalConfigurationModel Configuration { get; }
    }
}