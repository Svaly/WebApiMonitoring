using System.Threading.Tasks;

namespace Framework.Service.Cqrs
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
