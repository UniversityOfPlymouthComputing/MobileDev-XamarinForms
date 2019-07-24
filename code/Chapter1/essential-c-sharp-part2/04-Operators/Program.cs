using System;

namespace Operators
{
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        //Translate (math speak for add in this case) - returns a new Coordinate that is the sum
        public static Coordinate operator +(Coordinate u, Coordinate v) => new Coordinate(u.X + v.X, u.Y + v.Y);

        // Equality
        public static bool operator ==(Coordinate u, Coordinate v) => (u.X == v.X) && (u.Y == v.Y);
        public static bool operator !=(Coordinate u, Coordinate v) => !((u.X == v.X) && (u.Y == v.Y));
        public override bool Equals(object obj)
        {
            Coordinate u = obj as Coordinate;
            return (this.X == u.X) && (this.Y == u.Y);
        }
        public override int GetHashCode() => $"{X},{Y}".GetHashCode();
           
        // Implicit conversion (no typecast) - perform pythagoras when converting to a double
        public static implicit operator double(Coordinate u) => Math.Sqrt(u.X*u.X + u.Y*u.Y);
        public static implicit operator Coordinate(int d) => new Coordinate(d,d);

        // Explicit type conversions (where an explicit type-cast is provided)
        public static explicit operator Coordinate((int x, int y) tuple) => new Coordinate(tuple.x, tuple.y);

        public override string ToString() => $"x:{X},y:{Y}";
    }    
    class Program
    {
        static void Main(string[] args)
        {
            Coordinate p1 = new Coordinate(x: 3, y: 4);
            Coordinate p2 = new Coordinate(x: -1, y: 1);

            Console.WriteLine(p1+p2);

            //You write +, you get this for free!
            p1 += p2;
            Console.WriteLine($"P1 = {p1}");

            //The equality operator
            if (p1 != p2)
            {
                Console.WriteLine($"{p1} and {p2} are not equal");
            }

            //Implicit conversion
            double L1 = p1;
            Console.WriteLine($"Length of {p1} is {L1}");

            //This is a C# value-tuple (nice?)
            (int x, int y) t = (x: 3, y: 4);

            //Perform explicit conversion from a tuple to Coordinate
            Coordinate p3 = (Coordinate)t;
            Console.WriteLine($"p3 = {p3}");

            //Implicit conversion from integer to Coordinate
            Coordinate p4 = 0;
            Console.WriteLine($"p4 = {p4}");

        }
    }
}
