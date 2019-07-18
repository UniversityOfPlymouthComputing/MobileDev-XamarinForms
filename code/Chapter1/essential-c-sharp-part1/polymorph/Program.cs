using System;
using DepartmentOfTransport;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver commuter1 = new Driver(Name: "Regular Dave", new Car(EngineSerialNumber:12345,HasTowBarFitted:true));
            Driver commuter2 = new Driver(Name: "Risky Dave", new Motorbike(EngineSerialNumber:333555));
            Driver commuter3 = new Driver(Name: "Green Dave", null);

            Console.WriteLine(commuter1.Description);
            Console.WriteLine(commuter2.Description);
            Console.WriteLine(commuter3.Description);
        }
    }
}
