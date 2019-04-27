using System;

namespace Framework.Messaging.Exceptions
{
    [Serializable]
    public sealed class UnrecognizedRoutingKeyException : Exception
    {
        public UnrecognizedRoutingKeyException(string name)
            : base($"Unrecognized routing key: {name}")
        {
        }
    }
}