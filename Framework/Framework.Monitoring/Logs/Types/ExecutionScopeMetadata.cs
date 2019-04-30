﻿using System;

namespace Framework.Monitoring.Logs.Types
{
    public sealed class ExecutionScopeMetadata : IExecutionScopeMetadata
    {
        public string ApplicationName { get; set; }

        public Guid CausationId { get; set; }

        public Guid CorrelationId { get; set; }

        public ProcessingScope ProcessingScope { get; set; }
    }
}