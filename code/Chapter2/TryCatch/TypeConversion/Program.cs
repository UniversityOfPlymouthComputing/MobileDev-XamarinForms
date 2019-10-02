using System;

namespace TypeConversion
{
    public class BaseClass
    {
        double value { get; set; }
        public BaseClass(double u)
        {
            Console.WriteLine($"Constructor in BaseClass called");
            value = u;
        }

        virtual public void DoSomething()
        {
            Console.WriteLine("This is from the BaseClass");
        }
    }

    public class SubClass : BaseClass
    {
        public SubClass(double uu) : base(uu)
        {
            Console.WriteLine($"Constructor in the SubClass called");
        }

        override public void DoSomething()
        {
            Console.WriteLine("This is from the SubClass");
        }

    }
    class Program
    {
        public Program()
        {
            BaseClass obj1 = new BaseClass(2.0);
            obj1.DoSomething();

            SubClass obj2 = new SubClass(4.0);
            obj2.DoSomething();

            //This should throw an exception
            try
            {
                SubClass obj1_proxy = (SubClass)obj1; //Cast will throw an exception
                obj1_proxy.DoSomething();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program();
        }
    }
}
