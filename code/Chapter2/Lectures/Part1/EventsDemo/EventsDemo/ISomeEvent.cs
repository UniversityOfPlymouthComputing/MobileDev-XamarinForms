using System;

namespace EventsDemo
{
    public interface ISomeEvent {
        event EventHandler<string> SomeEvent;
    }
}
