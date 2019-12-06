using System;

namespace ValueReference
{
    class Program2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program2();
        }

        public Program2()
        {
            //Entry point

            // *******************
            // Reference semantics
            // *******************
            int a = 10;         //int is a value type

            //Equate b to a, but this time, b is a reference to a
            ref int b = ref a;

            Console.WriteLine($"a={a}, b={b}");

            //Now change a
            a += 1;
            Console.WriteLine($"Updating a to {a}");

            //So which has changed?
            Console.WriteLine($"a={a}, b={b}");

            //And the other way
            b += 10;
            Console.WriteLine($"Updating b to {b}");

            //Again, which has changed?
            Console.WriteLine($"a={a}, b={b}");

            //Once more, via a method
            updateInplace(ref a, 10);
            Console.WriteLine($"Updating a to {a}");
            Console.WriteLine($"a={a}, b={b}");
        }

        public void updateInplace(ref int u, int delta)
        {
            u += delta;
        }
    }
}
