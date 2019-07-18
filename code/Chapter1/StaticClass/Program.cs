using System;

namespace StaticClass
{

    static public class MathTools
    {
        static readonly double Pi = 3.1415926541;

        public static double Scale { get; set; } = 1.0;

        public static double AreaOfCircle(double radius)
        {
            double r = radius * Scale;
            return Pi * r * r;
        }
        public static double CircumferenceOfCircle(double radius)
        {
            double r = radius * Scale;
            return Pi * r * 2.0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Area of a circle radius 10m is " + MathTools.AreaOfCircle(10.0));
            Console.WriteLine("Circumference of a circle radius 10m is " + MathTools.CircumferenceOfCircle(10.0));
            MathTools.Scale = 0.01;
            Console.WriteLine("Area of a circle radius 10mm is " + MathTools.AreaOfCircle(10.0));
            Console.WriteLine("Circumference of a circle radius 10mm is " + MathTools.CircumferenceOfCircle(10.0));

        }
    }
}
