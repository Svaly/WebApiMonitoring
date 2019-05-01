using Framework.Patterns.Cqrs;
using Framework.Patterns.Loging;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.Monitoring;

namespace WebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        private readonly IMonitoringLogger _logger;

        protected BaseController(
            ICommandDispatcher commandDispatcher,
            IMonitoringLogger logger)
        {
            CommandDispatcher = commandDispatcher;
            _logger = logger;
        }

        public async Task<IHttpActionResult> HandleCommand<T>(T command)
            where T : ICommand
        {
            try
            {
                await CommandDispatcher.DispatchAsync(command);
                return Ok(command.CommandId);
            }
            catch (Exception e)
            {
                LogException(e);
                return InternalServerError(e);
            }
        }

        private void LogException(Exception e)
        {
            _logger.Log(new ExceptionLog(LogLevel.Error, e));
        }
    }
}