namespace AbstractClasses
{
    public class Gazelle : Animal, IHaveLegs, IEatVegetation
    {
        public uint LandSpeedIndex => 8;
        public uint NumberOfLegs => 4;

        public override void Breathe()
        {
            WriteStatus("Breathing through mouth with lungs");
        }

        public void MunchAndCrunch()
        {
            WriteStatus("Chew vigilently");
        }

        public void Run()
        {
            WriteStatus("Run, dodge and kick");
        }
    }


}
