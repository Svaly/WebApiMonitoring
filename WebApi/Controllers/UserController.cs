using Framework.Monitoring;
using Framework.Patterns.Cqrs;
using System.Threading.Tasks;
using System.Web.Http;
using Identity.Service.Contracts.Command;

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
