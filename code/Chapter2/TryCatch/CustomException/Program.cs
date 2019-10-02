using System;

namespace NestedErrors
{
    public class BadMathException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        public BadMathException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public BadMathException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public BadMathException(string message, System.Exception inner) : base(message, inner)
        {
        }

    }

    public class Program
    {
        private uint div(uint n, uint d)
        {
            if (d < 2)
            {
                throw new BadMathException($"You cannot divide by less than 2 in this algorithm. Divisor or value {d} was used.");
            } else
            {
                return n / d;
            }
        }
        uint f1(uint n, uint d)
        {
            uint dd = d / 2;
            uint nn = div(n, dd);
            return f2(nn, dd);
        }
        uint f2(uint n, uint d)
        {
            uint dd = d / 2;
            uint nn = div(n, dd);
            return f3(nn, dd);
        }
        uint f3(uint n, uint d)
        {
            uint dd = d / 2;
            uint nn = div(n, dd);
            return nn;
        }

        public Program()
        {

            try
            {
                uint y1 = f1(65536, 8);
                Console.WriteLine("Final result: {0}", y1);

                uint y2 = f1(65536, 4);
                Console.WriteLine("Final result: {0}", y2);
            }
            catch (BadMathException bme)
            {
                Console.WriteLine(bme.Message);
            }

            Console.WriteLine("End");

        }
        static void Main(string[] args)
        {
            _ = new Program();
        }
    }
}
