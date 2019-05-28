using System;
using System.Linq;

namespace Framework.Patterns.Loging
{
    public sealed class ProcessingScope
    {
        private static readonly string _request = "WebRequest";
        private static readonly string _event = "Event";

        private static readonly string[] AvailableProcessingScopes =
        {
            _request, _event
        };

        public ProcessingScope(string processingScope)
        {
            Validate(processingScope);

            Value = processingScope;
        }

        public static ProcessingScope WebRequest => new ProcessingScope(_request);

        public static ProcessingScope Event => new ProcessingScope(_event);

        public string Value { get; }

        private void Validate(string processingScope)
        {
            if (string.IsNullOrEmpty(processingScope))
                throw new ArgumentNullException(processingScope, "Processing scope cannot be null");

            if (!AvailableProcessingScopes.Contains(processingScope))
                throw new ArgumentException($"Invalid processing scope: {processingScope}");
        }
    }
}