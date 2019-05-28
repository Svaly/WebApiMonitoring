using System;

namespace Framework.Patterns.Cqrs
{
    public interface ICommand
    {
        Guid CommandId { get; }

        //void SetChainOfCallsMetadata(Guid correlationId, Guid causationId);

        // Guid CorrelationId { get; }

        // Guid CausationId { get; }
    }
}