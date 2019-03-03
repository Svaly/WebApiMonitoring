using Identity.Handlers.Command;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.Service.Cqrs;

namespace WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UserController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser(RegisterUserCommand command)
        {
            try
            {
                await _commandDispatcher.Handle(command);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
    }
}
