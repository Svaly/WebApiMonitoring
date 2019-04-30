using Framework.Monitoring.Logs.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Monitoring.Logs.Publisher
{
    public class EventLogLogsPublisher : IEventLogLogsPublisher
    {
        public Task CommitLogsAsync(IEnumerable<ILog> logs)
        {
            return Task.CompletedTask;
        }
    }
}
