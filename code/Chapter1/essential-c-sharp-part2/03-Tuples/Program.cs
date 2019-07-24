using System;

namespace Tuples
{
    class Program
    {
        public static (int, int) Flip(int xx, int yy)
        {
            return (yy, xx);
        }
        static void Main(string[] args)
        {
            //Unnamed
            var t1 = (2, 3);
            Console.WriteLine($"Unnamed Tuple t1 has values Item1={t1.Item1} and Item2={t1.Item2}");

            //Named
            var t2 = (x: 2, y: 3);
            Console.WriteLine($"Named tuple t2 has values x={t2.x} and y={t2.y}");

            if (t1 == t2)
            {
                Console.WriteLine($"t1 is equal to t2");
            }

            //Projection initializers
            double p = 2.0;
            double q = 3.0;
            double r = 5.0;
            var t3 = (p, q, r);
            Console.WriteLine($"Named tuple t3 has values p={t3.p}, q={t3.q} and r={t3.r}");

            //Named overrides projection
            var t4 = (x: p, y: q, z: r);
            Console.WriteLine($"Named tuple t4 has values x={t4.x}, y={t4.y} and z={t4.z}");

            //Explicit types
            (string, int, bool) t5 = ("Hello", 123, false);
            Console.WriteLine($"Tuple t5 = {t5}");

            (string name, int age, bool smoker) t6 = (name: "Dave", age:51, smoker:false);
            Console.WriteLine($"Tuple t6 = {t6}");

            //Using nullable
            (string name, int age, bool? smoker) t7 = (name: "Fred", age: 54, smoker: null);
            Console.WriteLine($"Tuple t7 = {t7}");

            (string name, int age, bool smoker)? t8 = (name: "Fred", age: 54, smoker: false);
            if (t8.HasValue)
            {
                Console.WriteLine($"Tuple t8 = {t8}");
            }

            //Returning a tuple type from a function
            (int x, int y) t9 = Program.Flip(xx: 2, yy: 4);
            Console.WriteLine($"Tuple t9 = {t9}");

            //Returning a tuple type and unpacking
            (int x, int y) = Program.Flip(xx: 2, yy: 4);
            Console.WriteLine($"Unpacking we get x={x} and y={y}");
        }
    }
}
