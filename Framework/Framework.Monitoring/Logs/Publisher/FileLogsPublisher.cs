using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Framework.Monitoring.Logs.Types;
using Newtonsoft.Json;

namespace Framework.Monitoring.Logs.Publisher
{
    public class FileLogsPublisher : IFileLogsPublisher
    {
        public Task CommitLogsAsync(IEnumerable<ILog> logs)
        {
            return Task.CompletedTask;
        }
    }
}