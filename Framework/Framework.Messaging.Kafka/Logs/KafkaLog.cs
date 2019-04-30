using Framework.Patterns.Loging;
using System;
using System.Collections.Generic;

namespace Framework.Messaging.Kafka.Logs
{
    public sealed class KafkaLog : Log
    {
        public KafkaLog(
            LogLevel logLevel,
            KafkaLogType kafkaLogType,
            string message,
            IEnumerable<string> errors,
            IEnumerable<Exception> exceptions,
            IEnumerable<string> debugMessages,
            IEnumerable<KeyValuePair<string, string>> failedMessages)
            : base(logLevel, message)
        {
            KafkaLogType = kafkaLogType.Value;
            Exceptions = exceptions;
            DebugMessages = debugMessages;
            FailedMessages = failedMessages;
            Errors = errors;
        }

        public string KafkaLogType { get; }

        public IEnumerable<Exception> Exceptions { get; }

        public IEnumerable<string> Errors { get; }

        public IEnumerable<string> DebugMessages { get; }

        public IEnumerable<KeyValuePair<string, string>> FailedMessages { get; }
    }
}