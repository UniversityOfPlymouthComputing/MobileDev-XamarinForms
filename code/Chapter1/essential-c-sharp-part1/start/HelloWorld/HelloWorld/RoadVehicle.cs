using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        public int EngineSerialNumber;               // instance member variable

        public RoadVehicle()
        {
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
}
