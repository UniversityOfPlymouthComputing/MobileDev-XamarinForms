using System;

namespace AnonymousFunctions
{
    public class DelegateCommon
    {
        //This simply creates a new datatype DoMath
        protected delegate int DoMath(int a, int b);

        //This method takes a delegate type as a parameter
        protected void doMathStuff(DoMath f)
        {
            int[] xx = { 2, 4, 6, 8 };
            int[] yy = { 1, 3, 5, 7 };
            for (int n = 0; n < xx.Length; n++)
            {
                //Note how the delegate is invoked
                int y = f(xx[n], yy[n]);
                Console.WriteLine(y.ToString());
            }
        }
    }
}
