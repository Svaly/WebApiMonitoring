using System.Threading.Tasks;
using System.Web.Http;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Monitoring;
using Identity.Domain.Contracts.Commands;

namespace WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        public UserController(ICommandDispatcher commandDispatcher, IMonitoringLogsPublisher logsPublisher)
            : base(commandDispatcher, logsPublisher)
        {
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser(RegisterUserCommand command)
        {
            return await HandleCommand(command);
        }
    }
}