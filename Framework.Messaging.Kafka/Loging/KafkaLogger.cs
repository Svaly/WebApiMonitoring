using Confluent.Kafka;
using Framework.Monitoring.Logs.Logger;
using Framework.Monitoring.Logs.Types;
using System;
using System.Text;

namespace Framework.Messaging.Kafka.Loging
{
    public sealed class KafkaLogger : IKafkaLogger
    {
        private const int SystemLogSize = 30000;
        private readonly ILogger _logger;
        private readonly StringBuilder _logMessageBuilder;
        private bool _hasError;

        public KafkaLogger(ILogger logger)
        {
            _logger = logger;
            _logMessageBuilder = new StringBuilder();
            _hasError = false;
        }

        public void AppendMessageToLog(LogMessage logMessage)
        {
            var message = $"{logMessage.Name}   {(int)logMessage.Level} - {logMessage.Level}      {logMessage.Facility}    {logMessage.Message}";
            _logMessageBuilder.AppendLine(message);
        }

        public void AppendErrorToLog(Error error)
        {
            var message = $"Kafka error occurred; Code: {(int)error.Code} {error.Code}   Reason: {error.Reason}";
            _logMessageBuilder.AppendLine(message);
            _hasError = true;
        }

        public void LogException(Exception exception, string messageKey, string message)
        {
          //  var log = new ExceptionLog(LogLevel.Error, exception, );
            //_logger.EnqueueLog((LogLevel)LogLevel.Error, exception, FormatLogMessage(messageKey, message));
        }

        public void LogWarning(Exception exception, string messageKey, string message)
        {
            //_logger.EnqueueLog((LogLevel)LogLevel.Warn, exception, FormatLogMessage(messageKey, message));
        }

        public void LogWarning(string messageKey, string message)
        {
            //_logger.EnqueueLog((LogLevel)LogLevel.Warn, FormatLogMessage(messageKey, message));
        }

        public void LogInfo(string messageKey, string message)
        {
            //_logger.EnqueueLog((LogLevel)LogLevel.Info, FormatLogMessage(messageKey, message));
        }

        public void CommitLogs()
        {
            var message = _logMessageBuilder.ToString();

            if (message.Length > SystemLogSize)
            {
                CommitLogInBatches(message);
            }
            else if (message.Length > 0)
            {
                CommitLog(message);
            }
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
            if (_hasError)
            {
               // _logger.Log((LogLevel)LogLevel.Error, message);
            }
            else
            {
              //  _logger.Log((LogLevel)LogLevel.Debug, message);
            }
        }

        private string FormatLogMessage(string messageKey, string message)
        {
            return $"{message} Message key: {messageKey}";
        }
    }
}