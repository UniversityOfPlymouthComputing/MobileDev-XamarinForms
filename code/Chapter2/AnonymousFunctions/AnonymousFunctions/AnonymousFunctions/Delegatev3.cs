using System;

namespace AnonymousFunctions
{
    public class Delegatev3 : DelegateCommon
    {
        public Delegatev3()
        {
            Console.WriteLine("Let's do some maths with C# v3 Delegates!");

            //C# Version 3 Delegate
            Console.WriteLine("First we add");
            DoMath del1_v2 = (int a, int b) => { return a + b; };
            //Note how a function is now passed as a parameter
            doMathStuff(del1_v2);

            Console.WriteLine("Then we multiply (inline)");
            doMathStuff((int a, int b) => { return a * b; });
            doMathStuff((int a, int b) => a * b); //Single statement return
            doMathStuff((a, b) => a * b); //Type inference (lambda)
        }
    }
}
