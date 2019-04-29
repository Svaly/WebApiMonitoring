using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Monitoring.Logs.Types;

namespace Framework.Monitoring.Logs.Publisher
{
    public interface IMessageQueueLogsPublisher
    {
        Task CommitLogsAsync(IEnumerable<ILog> logs);
    }
}