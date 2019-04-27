using Identity.Handlers.Commands;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.Monitoring.Logs.Logger;
using Framework.Patterns.Cqrs;

namespace WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        public UserController(ICommandDispatcher commandDispatcher, ILogger logger) 
            : base(commandDispatcher, logger)
        {
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser(RegisterUserCommand command)
        {
            return await HandleCommand(command);
        }
    }
}
