using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    class Motorbike : RoadVehicle
    {
        public bool HasSideCar { get; set; }

        public Motorbike(int EngineSerialNumber, int CarriageCapacity = 2, bool HasSideCar = false) : base(EngineSerialNumber, NumberOfWheels: 2, CarriageCapacity)
        {
            this.HasSideCar = HasSideCar;
            Console.WriteLine("MotorBike Constructor: type " + this.GetType().ToString());
        }

        public override string Description
        {
            get
            {
                return base.Description + ": Is of type Motorbike" + (HasSideCar ? " with sidecar attached" : ".");
            }
        }
    }
}
