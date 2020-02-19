using System;

namespace GenericTypes
{
    public interface ModelTypes
    {
        string Author { get; }
    }

    public class ModelType1 : ModelTypes
    {
        public string Name { get; set; } = "Anon";
        public int Age { get; set; } = 21;
        public string Author => "Me";
    }

    public class ModelType2 : ModelTypes
    {
        public string Desc { get; set; } = "Square";
        public double Area { get; set; } = 1.0;
        public string Author => "You";
    }

    public class ViewModel1
    {
        ModelType1 Model;
        public ViewModel1( )
        {
            Model = new ModelType1();
        }
    }

    public class ViewModel2
    {
        ModelType2 Model;
        public ViewModel2()
        {
            Model = new ModelType2();
        }
    }

    public class ViewModel<T> where T : new()
    {

        public T Model { get; set; }

        public ViewModel(T m)
        {
            Model = m;
        }
        public ViewModel()
        {
            Model = new T();
            Console.WriteLine($"{Model}");
        }
    }

    public class AnotherViewModel
    {
        public ModelTypes Model;
        public AnotherViewModel(ModelTypes m)
        {
            Model = m;
        }
        public AnotherViewModel()
        {
            //Model = new ????
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ViewModel<ModelType1> m = new ViewModel<ModelType1>();
            
        }
    }
}
