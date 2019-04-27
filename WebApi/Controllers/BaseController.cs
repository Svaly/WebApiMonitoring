using Framework.Monitoring.WebApi.Extensions;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.Monitoring.Logs;
using Framework.Monitoring.Logs.Logger;
using Framework.Monitoring.Logs.Types;
using Framework.Patterns.Cqrs;

namespace WebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger _logger;

        protected BaseController(ICommandDispatcher commandDispatcher, ILogger logger)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        public async Task<IHttpActionResult> HandleCommand<T>(T command) where T : ICommand
        {
            command.SetChainOfCallsMetadata(Request.GetCorrelationIdHeader(), Request.GetRequestIdHeader());

            try
            {
                await _commandDispatcher.DispatchAsync(command);
                return Ok(command.CommandId);
            }
            catch (Exception e)
            {
                LogException(e, command);
                return InternalServerError(e);
            }
        }

        private void LogException(Exception e, ICommand command)
        {
            _logger.EnqueueLog(new ExceptionLog(LogLevel.Error, e, command.CorrelationId, command.CausationId, ""));
        }
    }
}