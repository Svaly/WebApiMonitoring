using Framework.Monitoring.Logs.Logger;
using Framework.Monitoring.Logs.Types;
using Framework.Patterns.Cqrs;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger _logger;

        protected BaseController(
            ICommandDispatcher commandDispatcher,
            ILogger logger)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        public async Task<IHttpActionResult> HandleCommand<T>(T command) where T : ICommand
        {
           // command.SetChainOfCallsMetadata(Request.GetCorrelationIdHeader(), Request.GetRequestIdHeader());

            try
            {
                await _commandDispatcher.DispatchAsync(command);
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