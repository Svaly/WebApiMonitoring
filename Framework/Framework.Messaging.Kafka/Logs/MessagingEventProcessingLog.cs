using System;
using Framework.Patterns.Loging;

namespace Framework.Messaging.Kafka.Logs
{
    public class MessagingEventProcessingLog : Log
    {
        public MessagingEventProcessingLog(
            Guid eventId,
            string host,
            long processingTime,
            LogLevel logLevel)
            : base(logLevel)
        {
            EventId = eventId;
            Host = host;
            ProcessingTime = processingTime;
        }

        public Guid EventId { get; }

        public string Host { get; }

        public long ProcessingTime { get; }
    }
}