using System;

namespace AnonymousFunctions
{
    public class Delegatev2 : DelegateCommon
    {
        public Delegatev2()
        {
            Console.WriteLine("Let's do some maths with C# v2 Delegates!");

            //C# Version 2 Delegate
            Console.WriteLine("First we add");
            DoMath del1_v2 = delegate (int a, int b)
            {
                return a + b;
            };
            //Note how a function is now passed as a parameter
            doMathStuff(del1_v2);

            Console.WriteLine("Then we multiply");
            DoMath del2_v2 = delegate (int a, int b)
            {
                return a * b;
            };
            doMathStuff(del2_v2);

        }
    }
}
