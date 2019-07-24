using System;
using System.Threading.Tasks;

namespace ComputedProperties
{

    class Circle
    {
        private double? _pi;
        private double PI
        {
            get
            {
                if (_pi == null)
                {
                    _pi = DoBigLongCalculationOfPi();
                }
                return (double)_pi;
            }
        }

        //Simulate slow calculation of PI
        private double DoBigLongCalculationOfPi()
        {
            for (uint n = 0; n < uint.MaxValue; n++)
            {
                
            };
            return 3.1415926541;
        }
        public double Radius { get; set; }

        public double Diameter { get => 2.0 * Radius; }
        public double Circumference { get => PI * Diameter; }
        public double Area { get => PI * Radius * Radius; }

        public Circle(double Radius)
        {
            this.Radius = Radius;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Circle c1 = new Circle(3.0);
            Console.WriteLine($"A circle of radius {c1.Radius} has a diameter of {c1.Diameter}, circumference of {c1.Circumference} and area of {c1.Area}");
            c1.Radius = 4.0;
            Console.WriteLine($"A circle of radius {c1.Radius} has a diameter of {c1.Diameter}, circumference of {c1.Circumference} and area of {c1.Area}");

        }
    }
}
