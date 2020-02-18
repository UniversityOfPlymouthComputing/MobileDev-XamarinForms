using System;

namespace Capturing
{
    class Program
    {
        Func<int, int> GenerateAccFunc(int initialValue) {
            int acc = initialValue;
            return (int u) => {
                acc += u;
                return acc;
            };
        }

        public Program() {
            int y1, y2;
            Func<int, int> accFunc1 = GenerateAccFunc(10);
            Func<int, int> accFunc2 = GenerateAccFunc(1);
            accFunc1(2);
            accFunc2(-5);
            y1 = accFunc1(3);
            y2 = accFunc2(10);
            Console.WriteLine(y1);
            Console.WriteLine(y2);
        }

        static void Main(string[] args)
        {
            _ = new Program();
        }
    }
}
