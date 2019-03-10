using System;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.Loging;
using Framework.Monitoring.WebApi.Extensions;
using Framework.Service.Cqrs;

namespace WebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly ICommandDispatcher _commandDispatcher;
        private readonly IApplicationMonitoringLogger _logger;

        protected BaseController(ICommandDispatcher commandDispatcher, IApplicationMonitoringLogger logger)
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
                return InternalServerError();
            }
        }

        private void LogException(Exception e, ICommand command)
        {
            _logger.EnqueueLog(new ExceptionLog(e, command.CorrelationId, command.CausationId, ""));
        }
    }
}