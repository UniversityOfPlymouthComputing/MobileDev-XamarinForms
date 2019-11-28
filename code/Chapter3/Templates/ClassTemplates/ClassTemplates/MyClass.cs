using System;

// For System.Object, see https://docs.microsoft.com/dotnet/api/system.object?view=netframework-4.8

// See https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplates
{
    // Class classes derive from System.Object. We can only assume T is a derivative of System.Object
    public class MyClass<T>
    {
        T SomeObject1 { get; set; }
        T SomeObject2 { get; set; }
        public MyClass(T obj1, T obj2)
        {
            SomeObject1 = obj1;
            SomeObject2 = obj2;
            Ident();
            Comp();
            Console.WriteLine("-----------------");
        }
        private void Ident()
        {
            Console.WriteLine(SomeObject1.ToString());
            Console.WriteLine(SomeObject2.ToString());
        }
        private void Comp()
        {
            if (SomeObject1.Equals(SomeObject2))
            {
                Console.WriteLine("We are the same!");
            }
            else
            {
                Console.WriteLine("We are similar, but not the same object!");
            }
        }
    }
}
