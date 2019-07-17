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

            RoadVehicle v1 = new RoadVehicle();
            v1.EngineSerialNumber = 12345;

            RoadVehicle v2 = new RoadVehicle();
            v2.EngineSerialNumber = 2468;

            //Just to be sure
            if ((v1 == null) || (v2 == null))
            {
                System.Console.WriteLine("That all went badly!");
                return;
            }
            
            Console.WriteLine("Vehicle 1 serial: " + v1.EngineSerialNumber.ToString());
            Console.WriteLine("Vehicle 2 serial: " + v2.EngineSerialNumber.ToString());
        }
    }
}
