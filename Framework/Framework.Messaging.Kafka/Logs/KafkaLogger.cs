﻿using Confluent.Kafka;
using Framework.Patterns.Loging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Messaging.Kafka.Logs
{
    public sealed class KafkaLogger : IKafkaLogger
    {
        private readonly List<string> _debugMessages;
        private readonly List<string> _errors;
        private readonly List<Exception> _exceptions;
        private readonly List<KeyValuePair<string, string>> _failedMessages;
        private readonly ILogsPublisher _logsPublisher;

        public KafkaLogger(ILogsPublisher logsPublisher)
        {
            _logsPublisher = logsPublisher;
            _debugMessages = new List<string>();
            _errors = new List<string>();
            _exceptions = new List<Exception>();
            _failedMessages = new List<KeyValuePair<string, string>>();
        }

        public void AppendMessageToLog(LogMessage logMessage)
        {
            var message =
                $"{logMessage.Name}; {(int) logMessage.Level} - {logMessage.Level}; {logMessage.Facility}; {logMessage.Message};";
            _debugMessages.Add(message);
        }

        public void AppendErrorToLog(Error error)
        {
            var message = $"Kafka error occurred; Code: {(int) error.Code} {error.Code}; Reason: {error.Reason};";
            _errors.Add(message);
        }

        public void AppendExceptionToLog(Exception exception)
        {
            _exceptions.Add(exception);
        }

        public void AppendFailedMessageToLog(KeyValuePair<string, string> failedMessage)
        {
            _failedMessages.Add(failedMessage);
        }

        public void CommitLogs(KafkaLogType kafkaLogType, string message = "")
        {
            if (_exceptions.Any() || _errors.Any())
            {
                var log = new KafkaLog(
                    LogLevel.Error,
                    kafkaLogType,
                    message,
                    _errors.ToArray(),
                    _exceptions.ToArray(),
                    _debugMessages.ToArray(),
                    _failedMessages.ToArray());
                _logsPublisher.Publish(log);
            }
            else if (_debugMessages.Any())
            {
                var log = new KafkaLog(
                    LogLevel.Debug,
                    kafkaLogType,
                    message,
                    _errors.ToArray(),
                    _exceptions.ToArray(),
                    _debugMessages.ToArray(),
                    _failedMessages.ToArray());
                _logsPublisher.Publish(log);
            }

            _exceptions.Clear();
            _debugMessages.Clear();
            _errors.Clear();
            _failedMessages.Clear();
        }
    }
}