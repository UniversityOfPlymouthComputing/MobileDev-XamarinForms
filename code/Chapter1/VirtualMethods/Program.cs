using System;

namespace VirtualMethods
{
    public class Entity
    {
        public virtual void IdentifyYourself() => Console.WriteLine("I am Base");
    };

    public class TypeA : Entity
    {
        public override void IdentifyYourself() => Console.WriteLine("I am TypeA");
        public void JumpOverFences() => Console.WriteLine("Fence!");
    };

    public class TypeB : Entity
    {
        public new void IdentifyYourself() => Console.WriteLine("I am TypeB");
        public void JumpThroughHoops() => Console.WriteLine("Hoop!");
    };

    partial class Program
    {
        static bool FlipACoin() => false;
    }

    partial class Program
    {
        static void Main(string[] args)
        {
            Entity child;

            bool choice = FlipACoin(); // returns true or false
            if (choice == true)
            {
                child = new TypeA();
            }
            else
            {
                child = new TypeB();
            }

            child.IdentifyYourself();

            if (child is TypeA)
            {
                //First Cast to TypeA, then invoke
                ((TypeA)child).JumpOverFences();
            }
        }
    }
}
