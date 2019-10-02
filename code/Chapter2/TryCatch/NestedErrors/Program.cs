using System;

namespace NestedErrors
{
    public class Program
    { 
        uint f1(uint n, uint d)
        {
            uint dd = d / 2;
            uint nn = n / dd;
            return f2(nn, dd);
        }
        uint f2(uint n, uint d)
        {
            uint dd = d / 2;
            uint nn = n / dd;
            return f3(nn, dd);
        }
        uint f3(uint n, uint d)
        {
            uint dd = d / 2;
            uint nn = n / dd;
            return nn;
        }

        public Program()
        {
            uint y1 = f1(65536, 8);
            Console.WriteLine("Final result: {0}", y1);

            try
            {
                uint y2 = f1(65536, 4);
                Console.WriteLine("Final result: {0}", y2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Divide by zero");
            }

            Console.WriteLine("End");
           
        }
        static void Main(string[] args)
        {
            _ = new Program();
        }
    }
}
