using System;

// For System.Object, see https://docs.microsoft.com/en-us/dotnet/api/system.object?view=netframework-4.8

// See https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters

namespace ClassTemplates
{
    public class ClassA
    {
        protected int _data;
        public int Data { get => _data; set => _data = value; }

        public ClassA(int data)
        {
            Data = data;
        }

        // ToString() is to be found in System.Object
        public override string ToString() => "Hi there from class A!";
    }

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
        public override int GetHashCode() => base.GetHashCode();
        
    }

    // Class classes derive from System.Object. We can only assume T is a derivative of System.Object
    public class MyClass<T>
    {
        T SomeObject1 { get; set; }
        T SomeObject2 { get; set; }
        public MyClass(T obj1, T obj2)
        {
            SomeObject1 = obj1;
            SomeObject2 = obj2;
            Ident();
            Comp();
            Console.WriteLine("-----------------");
        }
        private void Ident()
        {
            Console.WriteLine(SomeObject1.ToString());
            Console.WriteLine(SomeObject2.ToString());
        }
        private void Comp()
        {
            if (SomeObject1.Equals(SomeObject2))
            {
                Console.WriteLine("We are the same!");
            }
            else
            {
                Console.WriteLine("We are similar, but not the same object!");
            }
        }
    }

    class Program
    {
        private Program()
        {
            ClassA objA1 = new ClassA(10);
            ClassA objA2 = new ClassA(10);
            _ = new MyClass<ClassA>(objA1, objA2);

            ClassB objB1 = new ClassB(10);
            ClassB objB2 = new ClassB(10);
            _ = new MyClass<ClassB>(objB1, objB2);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program();
        }
    }
}
