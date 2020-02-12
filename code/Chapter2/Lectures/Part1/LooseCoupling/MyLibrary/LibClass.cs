using System;

namespace MyLibrary
{
    public class LibClass
    {
        public IListener Originator { get; set; }
        public void Shout(string msg)
        {
            Console.WriteLine($"Library says {msg}");
            Originator?.ShoutBack("OK");
        }
    }
}
