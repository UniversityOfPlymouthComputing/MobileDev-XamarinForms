using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    class Motorbike : RoadVehicle
    {
        public bool HasSideCar { get; set; } = false;

        public Motorbike(int EngineSerialNumber, int NumberOfWheels = 2, int CarriageCapacity = 2, bool HasSideCarFitted = false) : base(EngineSerialNumber, NumberOfWheels:2, CarriageCapacity)
        {
            HasSideCar = HasSideCarFitted;
            Console.WriteLine("Motorbike Constructor: type " + this.GetType().ToString());
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
