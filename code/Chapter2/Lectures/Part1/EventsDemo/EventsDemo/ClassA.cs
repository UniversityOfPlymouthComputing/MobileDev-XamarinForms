using System;

namespace EventsDemo
{
    public class ClassA : ISomeEvent {
        public event EventHandler<string> SomeEvent;

        public void DeclarePi() {
            SomeEvent(this, "Pi is approx. 3.1415926541");
        }
    }
}
