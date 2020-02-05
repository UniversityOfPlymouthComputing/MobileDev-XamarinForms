using System;
using System.Collections.Generic;
using System.Text;

namespace PartialClass
{
    partial class BaseClass {
        protected string name;
        public string Name {
            get {
                return name;
            }
            protected set {
                if (value == name) return;
                name = value;
            }
        }
    }
}
