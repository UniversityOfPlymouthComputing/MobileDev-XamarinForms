using System;

namespace AnonymousFunctions
{
    public class Capturing
    {
        delegate double CounterFunction();

        CounterFunction CreateCounterFunction(double initValue = 0.0, double inc = 1.0)
        {
            double sum = initValue;

            //Captures both sum and inc
            double lbd() { sum += inc; return sum; }
            return lbd;

            // Some increasingly concise alternatives
            //return () => { sum += inc; return sum; };
            //return () => sum += inc;
        }

        // Side-stepping the need for a delegate type with Func and Action

        // Return type: Functon that returns a double and accepts no parameters
        Func<double> CreateFuncCounter(double initValue, double inc)
        {
            double sum = initValue;
            return () => { sum += inc; return sum; };
        }

        // Return type: Functon that returns a double and accepts a single double parameter
        Func<double,double> CreateFuncCounter(double initValue)
        {
            double sum = initValue;
            return u => { sum += u; return sum; };
        }

        //Action is similar, but constrained to only one parameter and no return type
        Action<double> CreateActionCounter(double initValue = 0.0)
        {
            double sum = initValue;
            return u => { sum += u; Console.WriteLine("Action: " + sum); };
        }

        public Capturing()
        {
            CounterFunction acc1 = CreateCounterFunction();
            CounterFunction acc2 = CreateCounterFunction(10.0, 2.0);
            var acc3 = CreateFuncCounter(100.0, 10.0); //Captures both init value and increment
            var acc4 = CreateFuncCounter(100.0); //Captures only the init value
            var acc5 = CreateActionCounter(); //Captures only the init value

            Console.WriteLine("Counter 1: " + acc1().ToString());
            Console.WriteLine("Counter 1: " + acc1().ToString());

            Console.WriteLine("Counter 2: " + acc2().ToString());
            Console.WriteLine("Counter 2: " + acc2().ToString());

            Console.WriteLine("Counter 3: " + acc3().ToString());
            Console.WriteLine("Counter 3: " + acc3().ToString());

            Console.WriteLine("Counter 4: " + acc4(5.0).ToString());
            Console.WriteLine("Counter 4: " + acc4(5.0).ToString());

            acc5(100.0);
            acc5(100.0);

        }
    }
}
