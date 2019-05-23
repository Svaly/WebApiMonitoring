using Catalog.Domain.Contracts.Commands;
using Framework.Monitoring;
using Framework.Patterns.Cqrs;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/catalog")]
    public class CatalogController : BaseController
    {
        public CatalogController(ICommandDispatcher commandDispatcher, IMonitoringLogsPublisher logsPublisher)
            : base(commandDispatcher, logsPublisher)
        {
        }

        [Route("addProductToCart")]
        [HttpPost]
        public async Task<IHttpActionResult> AddProductToCart(AddProductToCartCommand command)
        {
            return await HandleCommand(command);
        }

        [Route]
        [Route("likeProduct")]
        public async Task<IHttpActionResult> LikeProduct(LikeProductCommand command)
        {
            return await HandleCommand(command);
        }
    }
}
