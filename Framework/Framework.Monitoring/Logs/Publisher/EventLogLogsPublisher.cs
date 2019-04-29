using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Monitoring.Logs.Types;

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
