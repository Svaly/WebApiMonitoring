using Confluent.Kafka;
using System;
using System.Collections.Generic;

namespace Framework.Messaging.Kafka.Logs
{
    public interface IKafkaLogger
    {
        void AppendMessageToLog(LogMessage logMessage);

        void AppendErrorToLog(Error error);

        void AppendExceptionToLog(Exception exception);

        void AppendFailedMessageToLog(KeyValuePair<string, string> failedMessage);

        void CommitLogs(KafkaLogType kafkaLogType, string message = "");
    }
}