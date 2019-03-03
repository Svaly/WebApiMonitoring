using System.Threading.Tasks;

namespace Framework.Service.Cqrs
{
    public interface ICommandDispatcher
    {
        Task Handle<T>(T command) where T : ICommand;
    }
}
