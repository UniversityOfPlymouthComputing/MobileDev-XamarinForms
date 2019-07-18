using System;
using DepartmentOfTransport;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Code running: Project version " + RoadVehicle.ProjectVersion);

            RoadVehicle v1 = new RoadVehicle(EngineSerialNumber:12345);
            Console.WriteLine(v1.Description);

            RoadVehicle v2 = new RoadVehicle(EngineSerialNumber:2468);
            Console.WriteLine(v2.Description);
        }
    }
}
