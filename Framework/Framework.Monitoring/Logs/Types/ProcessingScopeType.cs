using System;
using System.Linq;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class ProcessingScopeType
    {
        private static readonly string _request = "WebRequest";
        private static readonly string _event = "Event";

        private static readonly string[] AvailableProcessingScopes =
        {
            _request, _event
        };

        public ProcessingScopeType(string processingScope)
        {
            Validate(processingScope);

            Value = processingScope;
        }

        public static ProcessingScopeType WebRequest => new ProcessingScopeType(_request);

        public static ProcessingScopeType Event => new ProcessingScopeType(_event);

        public string Value { get; }

        private void Validate(string processingScope)
        {
            if (string.IsNullOrEmpty(processingScope))
            {
                throw new ArgumentNullException(processingScope, "Processing scope cannot be null");
            }

            if (!AvailableProcessingScopes.Contains(processingScope))
            {
                throw new ArgumentException($"Invalid processing scope: {processingScope}");
            }
        }
    }
}