using System;

namespace AnonymousFunctions
{
    class Program
    {
        static void Main(string[] args) => _ = new Program();

        //This property needs to be assigned a concreme method
        public delegate int AddDelegate(int a, int b); //Declare a type

        AddDelegate _addDelegate;                      //Reference to an instance of AddDelegate

        public Program()
        {
            _addDelegate = AddThese;
            Console.WriteLine($"Delgate method returned {_addDelegate(2, 3)}");

            Function1();
            Console.WriteLine(Function2());
            //Call Function3, passing Function1 as a parameter
            Function3(Function1);
            Function4(Function2);

            Action a = () =>
            {
                Console.WriteLine("I am the anonymous function!");
            };
            Function3(a);

            Func<int, int, int> f = (int x1, int x2) => x1 + x2;

            int y1 = Function5(f, 3);
            Console.WriteLine(y1);

            //Pass in a lambda expression
            int y2 = Function5( (int p, int q) => p * q, 3);

            //Capturing a value
            Console.WriteLine("Capture behaviour - value type");
            int captureMe = 4;
            Action L1 = () =>
            {
                Console.WriteLine(captureMe * 2);
            };
            captureMe = 10;
            L1();

            //Currying
            var doScale = Scaler(10); //Returns a function
            int yy = doScale(5);
            Console.WriteLine(yy);
        }
        void Function1()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }


        bool Function2()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return ((DateTime.Now.Second % 2) == 0);
        }

        void Function3(Action a)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            a();
        }

        bool Function4(Func<bool> f)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return f();
        }

        int Function5(Func<int, int, int> f, int x)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return f(x, x * x);
        }

        //Currying
        Func<int, int> Scaler(int factor)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            //Capture `factor` and return function
            return (int u) => factor * u;
        }

        int AddThese(int x, int y) => x + y;
    }
}
