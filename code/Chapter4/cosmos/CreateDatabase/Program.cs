using System;
//using Newtonsoft.Json;

namespace CreateDatabase
{
    public class SolPlanet
    {
        
        public String ID { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public SolPlanet(string name, double dist) => (Name, Distance) = (name, dist);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
