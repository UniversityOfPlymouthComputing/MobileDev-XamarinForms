namespace AbstractClasses
{
    public class Cheetah : Animal, ICatchPrey, IHaveLegs
    {
        public uint PreditorIndex { get => 7; }
        public uint LandSpeedIndex { get => 10; }
        public uint NumberOfLegs { get => 4; }

        public void Run()
        {
            WriteStatus("Zoom");
        }

        public void SeekAndChasePrey()
        {
            WriteStatus("Crouch, sneak, pounce");
        }
    }

    public class BoxFish : Animal, ICanSwim
    {
        public BoxFish()
        {

        }

        public uint WaterSpeed => 2;

        public void Swim()
        {
            WriteStatus("Swish, slowly slowly");
        }
    }


}
