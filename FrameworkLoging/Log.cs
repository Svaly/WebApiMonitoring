using System;

namespace Framework.Loging
{
    public class Log : ILog
    {
        public Log(Guid correlationId, Guid causationId, Type type, string applicationName, string data)
        {
            Id = Guid.NewGuid();
            CorrelationId = correlationId;
            CausationId = causationId;
            Timestamp = DateTime.Now;
            Data = data;
            ApplicationName = applicationName;
            Type = type.ToString();
        }

        public Guid Id { get; }

        public Guid CorrelationId { get; }

        public Guid CausationId { get; }

        public DateTime Timestamp { get; }

        public string Type { get; }

        public string ApplicationName { get; }

        public string Data { get; }
    }
}
