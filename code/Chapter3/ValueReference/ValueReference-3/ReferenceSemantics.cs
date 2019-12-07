using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ValueReference
{
    public class ReferenceSemantics
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new ReferenceSemantics();
        }

        void Display(MyObj obj, [CallerMemberName]string memberName="") => Console.WriteLine(memberName + $" = {obj}");
        
        public ReferenceSemantics()
        {
            //Entry point

            // *******
            // Classes
            // *******
            MyObj r1 = new MyObj(2, 3);
            MyObj r2 = new MyObj(10, 20);
            MyObj r3 = r1;

            Display(r1,"r1");
            Display(r2,"r2");
            Display(r3, "r3");

            Console.WriteLine("Update r1");
            ReferenceSemantics.NegateInline(r1);
            //r1.a *= -1;
            //r1.b *= -1;

            Display(r1, "r1");
            Display(r2, "r2");
            Display(r3, "r3");

            // **********
            // Structures
            // **********
            Console.WriteLine("Structures");
            MyStruct s1 = new MyStruct(2, 3);
            MyStruct s2 = s1;
            Console.WriteLine($"s1: {s1}, s2: {s2}");
            s1.a *= -1;
            Console.WriteLine($"s1: {s1}, s2: {s2}");
        }

        // Parameter is a reference type, so can be modified inline
        static void NegateInline(MyObj obj)
        {
            obj.a *= -1;
            obj.b *= -1;
        }

    }

    public class MyObj 
    {
        public int a;
        public int b;
        public MyObj(int aa, int bb) => (a, b) = (aa, bb);
        public override string ToString() => $"a={a}, b={b}";
    }

    public struct MyStruct
    {
        public int a;
        public int b;
        public MyStruct(int aa, int bb) => (a, b) = (aa, bb);
        public override string ToString() => $"a={a}, b={b}";
    }
}
