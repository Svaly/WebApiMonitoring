using System;
using System.Linq;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class ProcessingScope
    {
        private static string _request = "Request";
        private static string _messageQueue = "MessageQueue";

        private static readonly string[] AvailableProcessingScopes =
        {
            _request, _messageQueue,
        };

        public ProcessingScope(string processingScope)
        {
            Validate(processingScope);

            Value = processingScope;
        }

        public static LogLevel Request => new LogLevel(_request);

        public static LogLevel MessageQueue => new LogLevel(_messageQueue);

        public string Value { get; }

        private void Validate(string processingScope)
        {
            if (string.IsNullOrEmpty(processingScope)) throw new ArgumentNullException(processingScope, "Processing scope cannot be null");

            if (!AvailableProcessingScopes.Contains(processingScope)) throw new ArgumentException($"Invalid processing scope: {processingScope}");
        }
    }
}
