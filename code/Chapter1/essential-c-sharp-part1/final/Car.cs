using System;

namespace XamarinCh1
{
    class Car : RoadVehicle
    {
        public bool HasTowBar { get; set; } = false;

        //Tow Bar can be retrospectively fitted or removed
        //This is dynamically created so is always up to date
        protected override string Description => base.Description + string.Format(", Towbar: " + (HasTowBar ? "Yes" : "No"));

        public override string ToString()
        {
            return base.ToString() + ",  " + Description;
        }
        public Car(int NumberOfWheels = 4, int CarriageCapacity = 5, bool HasTowBarFitted = false) : base(NumberOfWheels, CarriageCapacity)
        {
            HasTowBar = HasTowBarFitted;
            Console.WriteLine("Car Constructor: type " + this.GetType().ToString());
        }
    }
}
