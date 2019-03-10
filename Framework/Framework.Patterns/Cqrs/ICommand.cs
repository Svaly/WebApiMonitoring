using System;

namespace Framework.Service.Cqrs
{
    public interface ICommand
    {
        void SetChainOfCallsMetadata(Guid correlationId, Guid causationId);

         Guid CommandId { get; }
        
         Guid CorrelationId { get; }
        
         Guid CausationId { get; }
    }
}
