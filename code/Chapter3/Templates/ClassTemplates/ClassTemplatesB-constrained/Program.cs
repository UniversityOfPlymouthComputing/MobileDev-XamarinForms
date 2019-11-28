using System;
using System.Collections.Generic;

// https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplatesB_constrained
{
    // The constraint is that T must implement IComparable. Therefore we can perform a custom sort such objects in a collection
    public class MyClass<T> where T : IComparable
    {
        public List<T> Objects { get; } = new List<T>();

        public void AddObject(T obj)
        {
            Objects.Add(obj);
            Objects.Sort();
        }

        public void ListAll()
        {
            Console.WriteLine("Listing all objects in order of increasing magnitude");
            foreach (T obj in Objects)
            {
                Console.WriteLine(obj); //Calls ToString
            }
        }
    }

    class Program
    {
        private Program()
        {
            MyClass<ClassXY> container = new MyClass<ClassXY>();
            container?.AddObject(new ClassXY(3.0,4.0));
            container?.AddObject(new ClassXY(1.0, 10.0));
            container?.AddObject(new ClassXY(0.3, 0.4));
            container?.ListAll();

            /* 
            MyClass<Binary32> container1 = new MyClass<Binary32>();
            container1?.AddObject(new Binary32(16));   // 16  is 00010000b
            container1?.AddObject(new Binary32(255)); // 255 is 11111111b 
            container1?.AddObject(new Binary32(3));   // 3   is 00000011b
            container1?.ListAll();
            */

        }

        static void Main(string[] args)
        {
            _ = new Program();
        }
    }
}
