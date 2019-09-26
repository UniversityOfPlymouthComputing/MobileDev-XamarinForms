using System;

namespace AbstractClasses
{
    class GreenSeaTurtle : Animal, ICanSwim, IEatVegetation, IHaveLegs
    {
        public uint WaterSpeed => 3;
        public uint LandSpeedIndex => 1;
        public uint NumberOfLegs => 4;

        public void MunchAndCrunch()
        {
            WriteStatus("Nibble the weed tips");
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            WriteStatus("Gently wave fins, no rush");
        }
    }
}
