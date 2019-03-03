using System.Threading.Tasks;

namespace Framework.Service.Cqrs
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task  Execute(TCommand command);
    }
}
