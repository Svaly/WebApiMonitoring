using System;

namespace Framework.Patterns.Cqrs
{
    public interface ICommand
    {
        void SetChainOfCallsMetadata(Guid correlationId, Guid causationId);

         Guid CommandId { get; }
        
         Guid CorrelationId { get; }
        
         Guid CausationId { get; }
    }
}
