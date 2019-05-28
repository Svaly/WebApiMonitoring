using System;

namespace Framework.Messaging.Exceptions
{
    [Serializable]
    public sealed class MessageQueueUnreachableException : Exception
    {
        public MessageQueueUnreachableException(string connectionName, Exception innerException)
            : base(
                $"External message queue was unreachable when trying to create connection named: {connectionName}",
                innerException)
        {
        }
    }
}