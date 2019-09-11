using System;

namespace AnonymousFunctions
{
    public class Delegatev1 : DelegateCommon
    {
        //Some methods with the same type signature as DoMath
        int addSomeNumbers(int a, int b)
        {
            return a + b;
        }
        int mulSomeNumbers(int a, int b)
        {
            return a * b;
        }

        //Constructor
        public Delegatev1()
        {
            Console.WriteLine("Let's do some maths with C# v1 Delegates!");

            //C# Version 1 Delegate
            Console.WriteLine("First we add");
            DoMath del1_v1 = new DoMath(addSomeNumbers);
            //Note how a function is now passed as a parameter
            doMathStuff(del1_v1);

            Console.WriteLine("Then we multiply");
            DoMath del2_v1 = new DoMath(mulSomeNumbers);
            doMathStuff(del2_v1);
        }
    }
}
