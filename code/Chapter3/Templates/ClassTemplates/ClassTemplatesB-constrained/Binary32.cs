using System;

// https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplatesB_constrained
{
    //This class counts the number of binary '1's in an integer number
    public class Binary32 : IComparable
    {
        private int decimalValue = 0;
        public int NumberOfOnes { get; private set; }

        //Setting the decimal value will automatically recalculate the number of binary 1's
        public int DecimalValue
        {
            get => decimalValue;
            set
            {
                decimalValue = value;
                NumberOfOnes = 0;
                for (int b = 0; b < 32; b++)
                {
                    int bitMask = 1 << b;
                    NumberOfOnes += ((decimalValue & bitMask) == 0) ? 0 : 1;
                }
            }
        }

        //Used in sorting - Return +1 when this precedes obj, 0 for same, -1 for obj precedes this
        public int CompareTo(object obj)
        {
            if (obj is Binary32 other)
            {
                return (this.NumberOfOnes > other.NumberOfOnes) ? 1 : -1;
            }
            //Two different types have the same sort position
            return 0;
        }

        //ToString write the value as a binary number
        public override string ToString()
        {
            string result = "";
            for (int b = 31; b >= 0; b--)
            {
                int bitMask = 1 << b;
                result += ((decimalValue & bitMask) == 0) ? "0" : "1";
            }
            return result;
        }

        //Constructor
        public Binary32(int u) => DecimalValue = u;

    }
}
