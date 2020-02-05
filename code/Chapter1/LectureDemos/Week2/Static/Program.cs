using System;

namespace PartialClass
{
    class Program
    {
        static void Main(string[] args) {
            Student obj1 = new Student("Dave", 9876);
            obj1.Hello();

            Student obj2 = new Student("Les", 1234);
            obj2.Hello();

            Student.Institution = "Uni of Sealand";
            BaseClass obj3 = new BaseClass("Jo");
            obj3.Hello();
        }
    }
}
