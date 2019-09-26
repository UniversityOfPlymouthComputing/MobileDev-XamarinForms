using System;

namespace AbstractClasses
{
    public class Shark : Animal, ICatchPrey, ICanSwim
    {
        public uint PreditorIndex => 10;
        public uint WaterSpeed => 6;

        public override void Breathe()
        {
            WriteStatus("Breathing through gills. In essence, a big fish.");
        }

        public void SeekAndChasePrey()
        {
            WriteStatus("Dart, open mouth and snap");
        }

        public void Swim()
        {
            WriteStatus("Sprint and twist");
        }
    }


}
