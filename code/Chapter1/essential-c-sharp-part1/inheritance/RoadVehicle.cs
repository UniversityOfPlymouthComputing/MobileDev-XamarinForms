using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    partial class RoadVehicle
    {

        public RoadVehicle(int EngineSerialNumber, int NumberOfWheels=4, int CarriageCapacity=5)
        {
            this.EngineSerialNumber = EngineSerialNumber;
            this.NumberOfWheels = NumberOfWheels;
            this.CarriageCapacity = CarriageCapacity;
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
}
