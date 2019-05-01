using Framework.Patterns.Messaging;
using System;

namespace Identity.Domain.Events
{
    public sealed class NewUserEvent : Event
    {
        public NewUserEvent(
            Guid aggregateId,
            Guid? userId,
            long aggregateVersion,
            long eventVersion,
            Guid correlationId,
            Guid causationId,
            string applicationName)
            : base(
                aggregateId,
                userId,
                aggregateVersion,
                eventVersion,
                correlationId,
                causationId,
                applicationName)
        {
        }
    }
}