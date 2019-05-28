using System.Threading.Tasks;

namespace Framework.Patterns.Cqrs
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery
    {
        Task<TResult> Execute(TQuery query);
    }
}