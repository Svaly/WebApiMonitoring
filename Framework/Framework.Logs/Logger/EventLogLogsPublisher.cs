using System;
using System.Threading.Tasks;
using Framework.Patterns.Loging;
using Newtonsoft.Json;

namespace Framework.Logs.Logger
{
    public class EventLogLogsPublisher : IEventLogLogsPublisher
    {
        private const int SystemLogSize = 30000;

        public Task PublishAsync(ILog log)
        {
            var serializedLog = JsonConvert.SerializeObject(log);

            if (serializedLog.Length > SystemLogSize)
            {
                CommitLogInBatches(serializedLog);
            }
            else if (serializedLog.Length > 0)
            {
                CommitLog(serializedLog);
            }

            return Task.CompletedTask;
        }

        private void CommitLogInBatches(string message)
        {
            for (int i = 0; i < message.Length; i += SystemLogSize)
            {
                CommitLog(message.Substring(i, Math.Min(SystemLogSize, message.Length - i)));
            }
        }

        private void CommitLog(string message)
        {
        }
    }
}
