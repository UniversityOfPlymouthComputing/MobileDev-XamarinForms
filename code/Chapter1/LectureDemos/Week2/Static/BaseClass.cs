using System;
using System.Collections.Generic;
using System.Text;

namespace PartialClass
{
    partial class BaseClass {
        public BaseClass() {
            Console.WriteLine("BaseClass Parameterless Constructor");
            name = "Anon";
            instCount++;
        }
        public BaseClass(string name) : this() {
            Console.WriteLine("BaseClass Constructor with Name");
            Name = name;
        }

        public virtual void Hello() {
            Console.WriteLine($"{instCount} - From: {Institution}");
        }

        public static void WhereAmI() => Console.WriteLine(Institution);
        protected static int instCount = 0;
        public static string Institution { get; set; } = $"UoP";
    }
}
