using System;

namespace AndFinally
{
    class Program
    {
        public Program()
        {
            uint p = 10, q=1;
            uint y;
            try
            {
                Console.WriteLine("Attempting the division");
                y = p / q;
                //return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0} ", e.Message);
                //return;
            }
            finally
            {
                Console.WriteLine("Tidy Up - close files and network sockets");
            }

            Console.WriteLine("End of the code");
        }
        static void Main(string[] args)
        {
            _ = new Program();
        }
    }
}
