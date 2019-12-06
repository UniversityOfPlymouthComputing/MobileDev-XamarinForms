using System;

namespace ValueReference
{
    class Program1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program1();
        }

        public Program1()
        {
            //Entry point

            // ***************
            // Value semantics
            // ***************
            int a = 10;         //int is a value type

            //Equate b to a
            int b = a;

            Console.WriteLine($"a={a}, b={b}");

            //Now change a
            a+=1;
            Console.WriteLine($"Updating a to {a}");

            //So which has changed?
            Console.WriteLine($"a={a}, b={b}");
        }
    }
}
