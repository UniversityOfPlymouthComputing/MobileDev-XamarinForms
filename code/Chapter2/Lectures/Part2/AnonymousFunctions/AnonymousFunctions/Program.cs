using System;

namespace AnonymousFunctions
{
    class Program
    {
        int[] pp = { 1, 3, 5, 7, 9 };
        int[] qq = { 2, 4, 6, 8, 10 };
        int[] y = new int[5];

        void Process( Func<int,int,int> f) {
            for (int n = 0; n < pp.Length; n++) {
                y[n] = f(pp[n], qq[n]);
                Console.WriteLine(y[n]);
            }
        }
        int Add(int a, int b) {
            return a + b;
        }
        int Mul(int a, int b) {
            return a * b;
        }

        public Program()
        {
            //for (int n = 0; n < pp.Length; n++) {
            //    y[n] = Add(pp[n], qq[n]);
            //    Console.WriteLine(y[n]);
            //}

            Process(Add);
            Process(Mul);
            //Process( (int a, int b)=> {
            //    int y = a * (1 + b);
            //    return y;
            //} );
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program();
        }
    }
}
