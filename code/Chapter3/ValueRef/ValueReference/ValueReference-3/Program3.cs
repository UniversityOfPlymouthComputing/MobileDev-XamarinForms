using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ValueReference
{
    class Program3
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program3();
        }

        void Display(MyObj obj, [CallerMemberName]string memberName="") => Console.WriteLine(memberName + $" = {obj}");
        
        public Program3()
        {
            //Entry point

            // *******************
            // Reference semantics
            // *******************
            MyObj r1 = new MyObj(2, 3);
            MyObj r2 = new MyObj(10, 20);
            MyObj r3 = r1;

            Display(r1,"r1");
            Display(r2,"r2");
            Display(r3, "r3");

            Console.WriteLine("Update r1");
            r1.a *= -1;
            r1.b *= -1;

            Display(r1, "r1");
            Display(r2, "r2");
            Display(r3, "r3");

        }

    }

    public class MyObj 
    {
        public int a;
        public int b;
        public MyObj(int aa, int bb) => (a, b) = (aa, bb);
        public override string ToString() => $"a={a}, b={b}";
    }
}
