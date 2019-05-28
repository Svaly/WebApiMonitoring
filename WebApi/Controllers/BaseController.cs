using System;
using System.Threading.Tasks;
using System.Web.Http;
using Framework.Patterns.Cqrs;
using Framework.Patterns.Loging;
using Framework.Patterns.Monitoring;

namespace WebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        private readonly IMonitoringLogsPublisher _logsPublisher;
        protected readonly ICommandDispatcher CommandDispatcher;

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