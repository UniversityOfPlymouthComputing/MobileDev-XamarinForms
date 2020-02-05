using System;
using System.Collections.Generic;
using System.Text;

namespace PartialClass
{
    partial class BaseClass {
        public BaseClass() {
            name = "Anon";
            Console.WriteLine($"BaseClass Constructor: name={name}");
        }
        public BaseClass(string name) {
            Name = name;
        }

        public void Hello() {
            Console.WriteLine("Hello BaseClass");
        }
    }
}
