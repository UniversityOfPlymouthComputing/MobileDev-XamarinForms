using System;
using System.Collections.Generic;
using System.Text;

namespace PartialClass
{
    partial class BaseClass {
        /* Moved elsewhere
        protected string name;
        public string Name
        {
            get
            {
                return name;
            }
        }
        */
        public BaseClass() {
            name = "Anon";
            Console.WriteLine($"BaseClass Constructor: name={name}");
        }
        public BaseClass(string name) {
            Name = name;
        }
    }
}
