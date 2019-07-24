using System;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        class MyModel
        {
            private uint _called = 0;
            public void ExpensiveCompute()
            {
                for (uint u=0; u<uint.MaxValue/2; u++)
                {

                }
            }

            public async Task ParallelCompute()
            {
                await Task.Run( () => ExpensiveCompute() );
            }

        }

        public async Task RunAsync()
        {
            Console.WriteLine("Hello World!");

            MyModel m = new MyModel();
            Console.WriteLine("Starting 1..");
            m.ExpensiveCompute();
            Console.WriteLine("Done.");

            Console.WriteLine("Starting 2..");
            m.ExpensiveCompute();
            Console.WriteLine("Done.");

            Console.WriteLine("Starting 3..");
            m.ExpensiveCompute();
            Console.WriteLine("Done.");

            Console.WriteLine("Starting 4..");
            m.ExpensiveCompute();
            Console.WriteLine("Done.");

            Console.WriteLine("Starting 1,2,3,4..");
            Task t1 = m.ParallelCompute();  //Start task 1
            Task t2 = m.ParallelCompute();  
            Task t3 = m.ParallelCompute();
            Task t4 = m.ParallelCompute();
            await t1;
            await t2;
            await t3;
            await t4;
            Console.WriteLine("Done.");
        }
        static async Task Main(string[] args)
        {
            Program p = new Program();
            await p.RunAsync();
        }
    }
}
