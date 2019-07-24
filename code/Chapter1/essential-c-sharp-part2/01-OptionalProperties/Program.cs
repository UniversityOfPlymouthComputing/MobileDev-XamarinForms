using System;

namespace OptionalProperties
{
    class MyModel
    {
        public string FirstName { get; set; }

        public string KnownAs { get; set; }

        public int Age { get; set; }

        public uint? PhoneNumber { get; set; }

        public void Display()
        {
            string Str = $"This is {FirstName}";
            if (KnownAs != null)
            {
                Str += $", also known as {KnownAs}";
            }
            Str += $", Age {Age}";
            if (PhoneNumber != null)
            {
                Str += $", Phone number { PhoneNumber}";
            } 
            Console.WriteLine(Str);
        }

        public MyModel(string FirstName, int Age, string KnownAs=null, uint? PhoneNumber=null)
        {
            this.FirstName = FirstName;
            this.KnownAs = KnownAs;
            this.Age = Age;
            this.PhoneNumber = PhoneNumber;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyModel m = new MyModel(FirstName: "Brian", Age: 71);
            m.Display();
        }
    }
}
