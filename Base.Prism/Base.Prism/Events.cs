using Prism.Events;

namespace Base.Prism.Events
{
    public class ShuttingDownEvent : PubSubEvent<bool> { }

    public class ShutDownEvent : PubSubEvent<bool> { }

}
