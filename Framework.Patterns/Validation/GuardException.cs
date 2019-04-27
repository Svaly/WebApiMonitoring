using System;

namespace Framework.Patterns.Validation
{
    [Serializable]
    public sealed class GuardException : Exception
    {
        public GuardException(string message)
            : this(string.Empty, message)
        {
        }

        public GuardException(string paramName, string message)
            : base(message)
        {
            ParameterName = paramName;
        }

        public string ParameterName { get; }
    }
}