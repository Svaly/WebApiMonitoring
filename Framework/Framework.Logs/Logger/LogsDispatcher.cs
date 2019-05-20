using Framework.Patterns.Loging;
using System.Threading.Tasks;

namespace Framework.Logs.Logger
{
    public sealed class LogsDispatcher : ILogsDispatcher
    {
        private readonly IEventLogLogsPublisher _eventLogLogsPublisher;
        private readonly IMessageQueueLogsPublisher _messageQueueLogsPublisher;

        public LogsDispatcher(IEventLogLogsPublisher eventLogLogsPublisher, IMessageQueueLogsPublisher messageQueueLogsPublisher)
        {
            _eventLogLogsPublisher = eventLogLogsPublisher;
            _messageQueueLogsPublisher = messageQueueLogsPublisher;
        }

        public async Task DispatchAsync(ILog log)
        {
            await _messageQueueLogsPublisher.PublishAsync(log);

            //await Task.WhenAll(
            //    _messageQueueLogsPublisher.PublishAsync(log),
            //    _eventLogLogsPublisher.PublishAsync(log));
        }
    }
}