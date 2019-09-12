using System;

namespace AnonymousFunctions
{
    public class Capturing
    {
        // Return type: Functon that returns a double and accepts no parameters
        Func<double> CreateCounter(double initValue=0.0, double inc=1.0)
        {
            double sum = initValue;
            return () => { sum += inc; return sum; };
        }


        public Capturing()
        {
            Func<double> acc1 = CreateCounter();
            var acc2 = CreateCounter(10.0, 2.0);

            Console.WriteLine("Counter 1: " + acc1().ToString());
            Console.WriteLine("Counter 1: " + acc1().ToString());

            Console.WriteLine("Counter 2: " + acc2().ToString());
            Console.WriteLine("Counter 2: " + acc2().ToString());

        }
    }
}
