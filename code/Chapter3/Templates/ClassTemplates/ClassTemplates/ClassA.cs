// For System.Object, see https://docs.microsoft.com/dotnet/api/system.object?view=netframework-4.8

// See https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplates
{
    public class ClassA
    {
        protected int _data;
        public int Data { get => _data; set => _data = value; }

        public ClassA(int data) => Data = data;

        // ToString() is to be found in System.Object
        public override string ToString() => "Hi there from class A!";
    }
}
