using System;

namespace XamarinCh1
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("static void Main says Hello World!");

            RoadVehicle v1 = new RoadVehicle();
            Console.WriteLine(v1.ToString());

            Car v2 = new Car(NumberOfWheels: 3, HasTowBarFitted: true);
            Console.WriteLine(v2.ToString());

            MotorBike v3 = new MotorBike(CarriageCapacity: 2);
            Console.WriteLine(v3.ToString());

            Console.WriteLine(Car.ProjectVersionString);
            //Program p = new Program();
        }

        Program() {
            Console.WriteLine("This is the constructor of " + this.ToString());
        }
    }
}
