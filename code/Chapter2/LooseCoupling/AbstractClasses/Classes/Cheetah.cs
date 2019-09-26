namespace AbstractClasses
{
    public class Cheetah : Animal, ICatchPrey, IHaveLegs
    {
        public uint PreditorIndex { get => 7; }
        public uint LandSpeedIndex { get => 10; }
        public uint NumberOfLegs { get => 4; }

        public override void Breathe()
        {
            WriteStatus("Breathing through mouth with lungs");
        }

        public void Run()
        {
            WriteStatus("Zoom");
        }

        public void SeekAndChasePrey()
        {
            WriteStatus("Crouch, sneak, pounce");
        }
    }


}
