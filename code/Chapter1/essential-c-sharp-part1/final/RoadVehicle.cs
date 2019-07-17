using System;

namespace XamarinCh1
{
    partial class RoadVehicle
    {
        protected int _numberOfWheels;
        protected int _carriageCapacity;
        

        protected virtual string Description => string.Format("Wheels: {0:d}, Capacity: {1:d}", _numberOfWheels, _carriageCapacity);

        public override string ToString()
        {
            return base.ToString() + " : " + Description;
        }

        public RoadVehicle(int NumberOfWheels = 4, int CarriageCapacity = 5)
        {
            _numberOfWheels = NumberOfWheels;
            _carriageCapacity = CarriageCapacity;
            Console.WriteLine("RoadVehicle Constructor: type " + GetType().ToString());
        }
    }
}
