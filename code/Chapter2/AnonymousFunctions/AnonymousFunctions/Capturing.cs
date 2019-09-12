using System;

namespace AnonymousFunctions
{
    public class Capturing
    {

        void CapturingBehaviour()
        {
            Console.WriteLine("CapturingBehaviour");
            int scale = 10;

            //The following delegate 'captures' the variable scale
            Func<int,int> f1 = (u) => scale * u;

            int y1 = f1(5);
            Console.WriteLine($"{scale} * 5 = {y1}");

            scale = 2;
            int y2 = f1(5);
            Console.WriteLine($"{scale} * 5 = {y2}");
        }

        // Return type: Functon that returns a double and accepts no parameters
        Func<double> CreateCounter(double initValue, double inc)
        {
            double sum = initValue;
            return () => { sum += inc; return sum; };
        }


        public Capturing()
        {
            //Simple capturing behaviour
            CapturingBehaviour();

            //Return a function
            Func<double> acc1 = CreateCounter(0.0, 1.0);
            Func<double> acc2 = CreateCounter(10.0, 2.0);

            Console.WriteLine("Counter 1: " + acc1().ToString());
            Console.WriteLine("Counter 1: " + acc1().ToString());

            Console.WriteLine("Counter 2: " + acc2().ToString());
            Console.WriteLine("Counter 2: " + acc2().ToString());

        }
    }
}
