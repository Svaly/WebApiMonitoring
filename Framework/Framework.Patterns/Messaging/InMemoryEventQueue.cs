using System.Collections.Generic;

namespace Framework.Patterns.Messaging
{
    public class InMemoryEventQueue : Queue<IEvent>, IEventQueue
    {
        public bool HasEvents => Count > 0;
    }
}