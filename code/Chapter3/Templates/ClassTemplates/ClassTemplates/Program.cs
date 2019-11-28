using System;

// For System.Object, see https://docs.microsoft.com/dotnet/api/system.object?view=netframework-4.8

// See https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplates
{
    class Program
    {
        private Program()
        {
            ClassA objA1 = new ClassA(10);
            ClassA objA2 = new ClassA(10);
            _ = new MyClass<ClassA>(objA1, objA2);

            ClassB objB1 = new ClassB(10);
            ClassB objB2 = new ClassB(10);
            _ = new MyClass<ClassB>(objB1, objB2);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unbounded generics demo");
            _ = new Program();
        }
    }
}
