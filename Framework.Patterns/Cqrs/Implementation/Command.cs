using System;

namespace Framework.Patterns.Cqrs.Implementation
{
    public abstract class Command : ICommand
    {
        protected Command()
        {
            CommandId = Guid.NewGuid();
        }

        public void SetChainOfCallsMetadata(Guid correlationId, Guid causationId)
        {
            CorrelationId = correlationId;
            CausationId = causationId;
        }

        public Guid CommandId { get; }

        public Guid CorrelationId { get; private set; }

        public Guid CausationId { get; private set; }
    }
}
