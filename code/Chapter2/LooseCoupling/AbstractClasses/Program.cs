using System;
using System.Collections.Generic;

namespace AbstractClasses
{
    class Program
    {
        public Program()
        {
            Console.WriteLine("Adding all animals to a List");

            //Polymorphism - list of type Animal (abstract class type)
            List<Animal> animals = new List<Animal>
            {
                new Caterpillar(),
                new Cheetah(),
                new Gazelle(),
                new Shark(),
                new GreenSeaTurtle()
            };

            Console.WriteLine("\n\rBreathing Test");

            //Create a list for just preditors (ICatchPrey type)
            List<ICatchPrey> preditors = new List<ICatchPrey>();

            //Iterate over all animals
            foreach (var animal in animals)
            {
                //All animals breath (via gills or lungs)
                animal.Breathe();

                //Populate the preditor list
                if (animal is ICatchPrey p)
                {
                    preditors.Add(p);
                    if (p.GetType() == typeof(Cheetah))
                    {
                        Console.WriteLine("  (Nice kitty)");
                    }
                }
            }

            Console.WriteLine("\n\rIterating all preditors, most effective first");

            //Sort in-place, high to low
            preditors.Sort((x, y) => ((int)y.PreditorIndex - (int)x.PreditorIndex));

            foreach (var animal in preditors)
            {
                animal.SeekAndChasePrey();
            } 


        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program();
        }
    }
}
