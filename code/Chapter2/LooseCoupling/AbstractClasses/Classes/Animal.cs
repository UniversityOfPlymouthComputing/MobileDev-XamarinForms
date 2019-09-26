using System;

namespace AbstractClasses
{
    public abstract class Animal
    {
        protected void WriteStatus(String msg)
        {
            Console.WriteLine($"{this.GetType().Name}: {msg}");
        }
        public void Breathe()
        {
            WriteStatus("is Breathing");
        }

        public Animal()
        {
            WriteStatus("Constructor called");
        }
    }
}
