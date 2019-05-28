using System.Threading.Tasks;

namespace Framework.Patterns.Cqrs
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}