using System.Threading.Tasks;
using Framework.Service.Cqrs;
using Identity.Handlers.Commands;
using System.Web.Http;
using Framework.Loging;

namespace WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        public UserController(ICommandDispatcher commandDispatcher, IApplicationMonitoringLogger logger) 
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
