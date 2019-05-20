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
        private readonly IMonitoringLogsPublisher _logsPublisher;

        protected BaseController(
            ICommandDispatcher commandDispatcher,
            IMonitoringLogsPublisher logsPublisher)
        {
            CommandDispatcher = commandDispatcher;
            _logsPublisher = logsPublisher;
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
            _logsPublisher.Publish(new ExceptionLog(LogLevel.Error, e));
        }
    }
}