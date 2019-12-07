using System;

namespace ValueReference
{
    class Immutable
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Immutable();
        }

        public Immutable()
        {


            //Entry point
            string s1 = "Hello"; //equivalent to: string s1 = new string("Hello");  
            string s2 = s1;
            Console.WriteLine($"s1={s1}, s2={s2}");

            if (Object.ReferenceEquals(s1,s2))
            {
                Console.WriteLine("s1 and s2 are referencing the same object");
            } else
            {
                Console.WriteLine("s1 and s2 are indepdnent objects");
            }

            //Now modify s1
            Console.WriteLine("Modifying s1");
            s1 = "World";
            Console.WriteLine($"s1={s1}, s2={s2}");

            //Compare
            if (Object.ReferenceEquals(s1, s2))
            {
                Console.WriteLine("s1 and s2 are referencing the same object");
            }
            else
            {
                Console.WriteLine("s1 and s2 are indepdnent objects");
            }
            if (s1 == s2)
            {
                Console.WriteLine("s1 == s2");
            }
            else
            {
                Console.WriteLine("s1 != s2");
            }
            if (s1.Equals(s2))
            {
                Console.WriteLine("s1 Equals s2");
            }
            else
            {
                Console.WriteLine("s1 does not Equal s2");
            }

            UpdateString1(s1);
            Console.WriteLine($"s1={s1}, s2={s2}");

            UpdateString2(ref s1);
            Console.WriteLine($"s1={s1}, s2={s2}");

        }

        //Note the compiler warnings with this one
        void UpdateString1(string s)
        {
            s = "ABC";
        }

        //This has no compiler warning
        void UpdateString2(ref string s)
        {
            s = "ABC";
        }

    }
}
