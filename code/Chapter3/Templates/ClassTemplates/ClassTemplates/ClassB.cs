// For System.Object, see https://docs.microsoft.com/dotnet/api/system.object?view=netframework-4.8

// See https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplates
{
    public class ClassB : ClassA
    {
        public ClassB(int data) : base(data) { }

        // Equals() is to be found in System.Object
        public override bool Equals(object obj)
        {
            //Different type?
            if (obj.GetType() != this.GetType()) return false;

            //Same type with different data?
            ClassB other = (ClassB)obj;
            if (other.Data != Data) return false;

            return true;
        }

        public override string ToString() => "Greetings from class B";
        //Not so great hash code
        public override int GetHashCode()
        {
            return base.GetHashCode() + 27 * Data.GetHashCode();
        }
    }
}
