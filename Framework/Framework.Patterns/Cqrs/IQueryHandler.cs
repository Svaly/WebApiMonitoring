using System.Threading.Tasks;

namespace Framework.Service.Cqrs
{
    public interface IQueryHandler<in TQuery, TResult> 
        where TQuery : IQuery
    {
        Task<TResult> Execute(TQuery query);
    }
}
