using System;

namespace PartialClass
{
    class Program
    {
        static void Main(string[] args) {
            BaseClass obj1 = null;

            var r = new Random().Next(0,3);
            if (r == 0) {
                obj1 = new Student("Dave", 9876);
            } else if (r==1) {
                obj1 = new BaseClass();
            } else {
                Console.WriteLine("WARNING: NULL");
            }

            //int sn = obj1.StudentNumber; //Compiler won't allow

            //Method 1
            if (obj1?.GetType() == typeof(Student)) {
                Student st = (Student)obj1;
                int sn = st.StudentNumber; //Compiler will allow
                Console.WriteLine(sn);
            }

            //Method 2
            if (obj1 is Student s) {
                Console.WriteLine($"Type Student: {s.StudentNumber}");
            }
            if (obj1 is BaseClass b) {
                Console.WriteLine($"Type BaseClass: {b.Name}");
            }

            //Will this do a run time check?
            obj1?.Hello();
        }
    }
}
