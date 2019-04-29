using Framework.Monitoring.Logs.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Monitoring.Logs.Publisher
{
    public interface IEventLogLogsPublisher
    {
        Task CommitLogsAsync(IEnumerable<ILog> logs);
    }
}