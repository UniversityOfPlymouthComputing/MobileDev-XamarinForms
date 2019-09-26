namespace AbstractClasses
{
    public interface ICatchPrey
    {
        uint PreditorIndex { get; }
        void SeekAndChasePrey();
    }
}
