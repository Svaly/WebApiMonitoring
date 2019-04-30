using System.Threading.Tasks;

namespace Framework.Patterns.Cqrs
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
