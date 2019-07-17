using System;

namespace XamarinCh1
{
    class MotorBike : RoadVehicle
    {
        public MotorBike(int CarriageCapacity) : base(NumberOfWheels: 2, CarriageCapacity: CarriageCapacity)
        {
            Console.WriteLine("MotorBike Constructor: type " + this.GetType().ToString());
        }
    }
}
