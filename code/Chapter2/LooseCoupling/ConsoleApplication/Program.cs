using System;
using MyLibrary;

namespace ConsoleApplication
{
    class Program : IMessageLogger //IMessageLogger is declared in the library
    {
        public Program()
        {
            //This is the useful entry point of the application
            //Note how a reference to this is passed by parameter
            LibaryClass lib = new LibaryClass(this);
            lib.DoUsefulThing();
        }

        string AssemblyName => GetType().Assembly.GetName().Name;

        public void LogMessage(string msg)
        {
            string message = $"{AssemblyName}: {msg}";
            //Local function
            void banner()
            {
                foreach (char _ in message)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }

            //Write console message
            banner();
            Console.WriteLine(message);
            banner();
        }

        public void Complete(bool b)
        {
            if (b)
            {
                Console.WriteLine($"{AssemblyName}: All done");
            }
            else
            {
                Console.WriteLine($"{AssemblyName}: Did not complete");
            }
        }

        static void Main(string[] args) => _ = new Program();

    }
}
