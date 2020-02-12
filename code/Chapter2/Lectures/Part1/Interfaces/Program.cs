using System;
using System.Collections.Generic;

namespace AbstractClass
{
    public enum FuelType
    {
        Electric,
        Fossil,
        Organic,
        Chemical,
        Nuclear,
        None
    }

    public interface IHasWheels {
        int NumberOfWheels { get; set; }
    }

    public interface IPerformsSelfDiagnostics {
        bool PerformSelfCheck();
    }

    public interface IJumpsThroughHoops {
        void JumpThroughRingofFire();
    }

    public abstract class RoadVehicle : IHasWheels, IPerformsSelfDiagnostics {
        public int NumberOfWheels { get; set; }

        protected string Serial { get; set; }
        public FuelType Fuel { get; set; }

        public abstract bool PerformSelfCheck();

        public RoadVehicle() => throw new NotImplementedException("Parameterless constructor not permitted");

        public RoadVehicle(FuelType fuelType, string serialNumber, int numberOfWheels) {
            this.Serial = serialNumber;
            this.Fuel = fuelType;
            this.NumberOfWheels = numberOfWheels;
        }
    }

    public class Car : RoadVehicle {
        public Car(FuelType fuelType, string serialNumber) : base(fuelType, serialNumber, 4) {
            Console.WriteLine($"Car Constructor: Fuel:{fuelType}, SN: {serialNumber}");
        }

        public override bool PerformSelfCheck() {
            Console.WriteLine($"Check all {NumberOfWheels} wheels");
            Console.WriteLine("Check seat belts");
            Console.WriteLine("Check air bags");
            return true;
        }
    }

    public class MotorBike : RoadVehicle, IJumpsThroughHoops {
        public MotorBike(FuelType fuelType, string serialNumber) : base(fuelType, serialNumber, 2) {
            Console.WriteLine($"Bike Constructor: Fuel:{fuelType}, SN: {serialNumber}");
        }

        public void JumpThroughRingofFire() {
            Console.WriteLine("Drum Roll Please...");
        }

        public override bool PerformSelfCheck() {
            Console.WriteLine($"Check all {NumberOfWheels} wheels");
            Console.WriteLine("Check chain tension");
            Console.WriteLine("Check front brake");
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<RoadVehicle> SecondHandVehicles = new List<RoadVehicle>();
            SecondHandVehicles.Add(new Car(FuelType.Fossil, "012345"));
            SecondHandVehicles.Add(new Car(FuelType.Fossil, "02468A"));
            SecondHandVehicles.Add(new MotorBike(FuelType.Fossil, "13579"));

            foreach (RoadVehicle v in SecondHandVehicles) {
                Console.WriteLine("Checking type " + v.GetType());
                if (v is IJumpsThroughHoops crazy) {
                    crazy.JumpThroughRingofFire();
                }
            }
        }
    }
}
