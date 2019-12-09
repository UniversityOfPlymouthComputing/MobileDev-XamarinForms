using System;

namespace ValueReference
{
    public class DataModel
    {
        //Immutable reference type
        private string _stringData; 
        public string StringData {
            get => _stringData;
            set
            {
                if (_stringData == value) return;
                _stringData = value;
            }
        }
        //Mutable value-type
        public int IntData { get; set; } = 0;

        //Make it easy to display
        public override string ToString() => $"{StringData}, {IntData}";

        //Constructor
        public DataModel(string str, int u) => (StringData, IntData) = (str,u);
    }

    class Encapsulate
    {
        static void Main(string[] args) => _ = new Encapsulate();

        public Encapsulate()
        {
            //Entry point
            DataModel m1 = new DataModel("Hello",99);
            Console.WriteLine($"Model: {m1}");
            UpdateDataInline(m1);
            Console.WriteLine($"Model: {m1}");
        }

        //Pass a reference type as a parameter
        void UpdateDataInline(DataModel model)
        {
            Console.WriteLine("Updating model data");

            //Update the contents of the model
            model.StringData += " World";
            model.IntData += 1;
        }

    }
}
