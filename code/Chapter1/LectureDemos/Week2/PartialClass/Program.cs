using System;

namespace PartialClass
{
    class Program
    {
        static void Main(string[] args) {
            BaseClass obj1 = new BaseClass();
            Console.WriteLine($"Main: The name property of obj1 is {obj1.Name}");

            BaseClass obj2 = new BaseClass("Dave");
            Console.WriteLine($"Main: The name property of obj2 is {obj2.Name}");

            Student obj3 = new Student("Viv", 12345);
            Console.WriteLine($"Main: The Name property of obj3 is {obj3.Name}");
            Console.WriteLine($"Main: The StudentNumber property of obj3 is {obj3.StudentNumber}");

            Console.WriteLine($"typeof Student: {typeof(Student)}");        //Compile time
            Console.WriteLine($"GetType obj3: {obj3.GetType()}");           //Run time 
            Console.WriteLine($"obj3 is a Student?: {obj3 is Student}");    //Run time
            Console.WriteLine($"obj3 is a BaseClass?: {obj3 is BaseClass}");
        }
    }
}
