using System;

namespace ClassTemplatesC_MVVM
{
    public interface ModelTypes {
        string Author { get; }
    }
    public class ModelType1 : ModelTypes {
        public string Name { get; set; } = "Anon";
        public int Age { get; set; } = 21;
        public string Author => "Me";
    }
    public class ModelType2 : ModelTypes {
        public string Desc { get; set; } = "Square";
        public double Area { get; set; } = 1.0;
        public string Author => "You";
    }
    public class ViewModel1 {
        ModelType1 Model;
        public ViewModel1(ModelType1 m) {
            Model = m;
        }
    }
    public class ViewModel2 {
        ModelType2 Model;
        public ViewModel2(ModelType2 m) {
            Model = m;
        }
    }
    public class ViewModel<T> where T : ModelTypes {
        public T Model { get; set; }
        public ViewModel(T ModelObject) {
            Model = ModelObject;
            Console.WriteLine($"{Model.Author}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var m1 = new ModelType1();
            var m2 = new ModelType2();
            var obj1 = new ViewModel<ModelType1>(m1);
            var obj2 = new ViewModel<ModelType2>(m2);
        }
    }
}
