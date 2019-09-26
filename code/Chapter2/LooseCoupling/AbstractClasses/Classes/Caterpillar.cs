namespace AbstractClasses
{
    public class Caterpillar : Animal, IHaveLegs, IEatVegetation
    {
        public uint LandSpeedIndex => 1;
        public uint NumberOfLegs => 16;

        public override void Breathe()
        {
            WriteStatus("Breathing through spiracles and without lungs");
        }

        public void MunchAndCrunch()
        {
            WriteStatus("Mmmmm, lettuce");
        }

        public void Run()
        {
            WriteStatus("Time is but an illusion");
        }
    }
}
