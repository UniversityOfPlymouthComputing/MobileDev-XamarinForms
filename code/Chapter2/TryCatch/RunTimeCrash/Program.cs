using System;
using System.Collections.Generic;

namespace TryCatch
{
    class Program
    {
        public Program()
        {
            Dictionary<string, uint> lookup = new Dictionary<string, uint>();

            //Add is used for new items that do not already exist
            lookup.Add("Life", 42);
            lookup.Add("Loudest", 10);
            lookup.Add("LouderStill", 11);

            // ********************************
            // CASE 1 - null argument exception
            // lookup.Add(null, 1066);
            // ********************************

            //Does not throw an exception
            if (lookup.TryAdd("Life", 21) == false)
            {
                Console.WriteLine("Was not able to replicate the `Life` key");
            }

            // ********************************
            // CASE 2 - duplicate key
            // lookup.Add("Life", 21);
            // ********************************

            //Assumes key exists - throws exception if not
            uint u = lookup["Life"];

            //No exception thrown - check performed by developer
            if (lookup.TryGetValue("Life", out uint w))
            {
                Console.WriteLine($"The answer..{w}");
            }
            else
            {
                Console.WriteLine($"No such key");
            }

            // ********************************
            // CASE 3 - Key does not exist
            // v = lookup["LifeOnVenus"];
            // ********************************


            uint uu = 1; uint vv = 10;
            // ********************************
            // CASE 4 - Integer Divide by Zero
            //uint y = vv / (uu - 1);
            // ********************************


            List<string> mylist = new List<string>();
            mylist.Add("Hello");
            // ********************************
            // CASE 5 - Null Reference
            // mylist = null;
            // ********************************
            mylist.Add("World");

            Console.WriteLine("***********Done***********");
        }

        static void Main(string[] args)
        {
            _ = new Program();
        }
    }
}
