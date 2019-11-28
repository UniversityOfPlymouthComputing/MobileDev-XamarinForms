using System;

// https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplatesB_constrained
{
    public class ClassXY : IComparable
    {
        private readonly double xx;
        private readonly double yy;
        private double? _mag;       //nullable
        public double Magnitude
        {
            get
            {
                //Has a concrete value been calculated before?
                // Nice technique if an expensive calculation may turn out to be unneeded
                // The square root is not especially expensive of course, unless you perform it multiple times
                // See also Lazy<> for object instantiation on demand

                if (!_mag.HasValue)
                {
                    //Remember Pythagoras? - only calculated once and only on demand
                    _mag = Math.Sqrt(xx * xx + yy * yy);
                }
                return (double)_mag;
            }
        }

        //C# 7 has a nice compact way to initialise properties
        public ClassXY(double x, double y) => (xx, yy) = (x, y);

        // ToString() is to be found in System.Object
        public override string ToString() => string.Format("ClassXY: |({0:F3},{1:F3})| = {2:F3}", xx, yy, Magnitude);

        //Used in sorting - Return +1 when this precedes obj, 0 for same, -1 for obj precedes this
        public int CompareTo(object obj)
        {
            // other = (ClassXY)obj IF obj is of type ClassXY
            // for details on patttern matching with is, see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/is
            if (obj is ClassXY other)
            {
                return (Magnitude > other.Magnitude) ? 1 : -1;
            }
            //Two different types have the same sort position (weird I know)
            return 0;
        }
    }
}
