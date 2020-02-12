using System;

namespace EventsDemo
{
    public class ClassB {
        public ClassB(ISomeEvent helper) {
            helper.SomeEvent += Aref_SomeEvent;
        }

        private void Aref_SomeEvent(object sender, string e) {
            Console.WriteLine($"I just heard from A, they said {e}");
        }
    }
}
