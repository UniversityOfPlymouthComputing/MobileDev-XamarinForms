using System;
using System.Collections.Generic;

namespace TryCatch
{
    class Program
    {
        public Program()
        {
            Dictionary<string, uint> lookup = new Dictionary<string, uint>();
            try
            {
                //Add is used for new items that do not already exist
                lookup.Add("Life", 42);
                lookup.Add("Loudest", 10);
                lookup.Add("LouderStill", 11);
                //return;  //early return
                //lookup.Add(null, 1066);

                //Does not throw an exception
                if (lookup.TryAdd("Life", 21) == false) {
                    Console.WriteLine("Was not able to replicate the Life key");
                }

                //Will throw an exception (duplicate key)
                lookup.Add("Life", 21); 
                
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine("*** key cannot be null ***");
                Console.WriteLine(anex.StackTrace);
            }
            catch (ArgumentException aex)
            {
                Console.WriteLine("{0}: {1}", aex.GetType().Name, aex.Message);
                Console.WriteLine("***********PROGRAM STACK TRACE***********");
                Console.WriteLine(aex.StackTrace);
                Console.WriteLine("***********PROGRAM STACK TRACE***********");
                //throw;    //Rethrow so outer try-catch can pick it up
            }
            finally
            {
                Console.WriteLine("***********Tidy Up***********");
            }

            //Failed lookup
            try
            {
                //Assumes key exists - throws exception if not
                uint u = lookup["Life"];

                //No exception thrown - check performed by developer
                if (lookup.TryGetValue("Life", out uint w))
                {
                    Console.WriteLine($"The answer..{w}");
                }

                //Key does not exist, so this throws an exception
                uint v = lookup["LifeOnVenus"];
                
            }
            catch (Exception)
            {
                Console.WriteLine($"Lookup failed");
                lookup["LifeOnVenus"]=84;
            }

            Console.WriteLine("***********Done***********");
        }

        static void Main(string[] args)
        {
            try
            {
                _ = new Program();
            }
            catch (Exception e)
            {
                Console.WriteLine("***********MAIN STACK TRACE***********");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("***********MAIN STACK TRACE***********");
            }
            
        }
    }
}
