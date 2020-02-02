using System;
namespace uoplib
{
    public class TestClass
    {
        public string Name { get; private set; }
        public TestClass(string s)
        {
            Name = s;
        }
        public override string ToString()
        {
            return $"My Name: {Name}";
        }
    }
}
