using System;
using Confluent.Kafka;

namespace Framework.Messaging.Kafka.Loging
{
    public interface IKafkaLogger
    {
        void AppendMessageToLog(LogMessage logMessage);

        void AppendErrorToLog(Error error);

        void LogException(Exception exception, string messageKey, string message);

        void LogWarning(Exception exception, string messageKey, string message);

        void LogWarning(string messageKey, string message);

        void LogInfo(string messageKey, string message);

        void CommitLogs();
    }
}