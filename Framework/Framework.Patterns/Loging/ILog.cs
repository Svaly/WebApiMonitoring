﻿using System;

namespace Framework.Patterns.Loging
{
    public interface ILog
    {
        Guid LogId { get; }

        string LogType { get; }

        DateTime Timestamp { get; }

        string LogLevel { get; }

        Guid CorrelationId { get; set; }

        Guid CausationId { get; set; }

        string ApplicationName { get; set; }

        string ProcessingScope { get; set; }
    }
}