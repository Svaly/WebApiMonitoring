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
        private static readonly ReaderWriterLock locker = new ReaderWriterLock();

        public Task CommitLogsAsync(IEnumerable<ILog> logs)
        {
            var tasks = new List<Task>();

            foreach (var log in logs)
            {
                tasks.Add(WriteToFile(log));
            }

            return Task.WhenAll(tasks);
        }

        private Task WriteToFile(object log)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);
                using (var file = new StreamWriter(@"F:\logs.txt", true))
                {
                    return file.WriteLineAsync(JsonConvert.SerializeObject(log));
                }
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }
    }
}