using System;

namespace AnonymousFunctions
{

    public class Funk
    {
        //This method takes a delegate type as a parameter
        protected void doMathStuff(Func<int, int, double> f)
        {
            int[] xx = { 2, 4, 6, 8 };
            int[] yy = { 1, 3, 5, 7 };
            double sum = 0.0;
            for (int n = 0; n < xx.Length; n++)
            {
                //Note how the delegate is invoked
                sum += f(xx[n], yy[n]);
            }
            Console.WriteLine($"{sum}");
        }
        public Funk()
        {
            Console.WriteLine("Now using Func<> " +
                "we calculate the sum of products");
            doMathStuff( (a, b) => (double)a * (double)b );
        }
    }
}
