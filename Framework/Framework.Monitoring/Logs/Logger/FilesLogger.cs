using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Framework.Monitoring.Logs.Types;
using Newtonsoft.Json;

namespace Framework.Monitoring.Logs.Logger
{
    public class FilesLogger : IFilesLogger
    {
        private static readonly ReaderWriterLock locker = new ReaderWriterLock();
        private readonly Queue<ILog> _logs;

        public FilesLogger()
        {
            _logs = new Queue<ILog>();
        }

        public void EnqueueLog(ILog log)
        {
            _logs.Enqueue(log);
        }

        public Task CommitLogsAsync()
        {
            var tasks = new List<Task>();

            while (_logs.Count > 0)
                tasks.Add(WriteToFile(_logs.Dequeue()));

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