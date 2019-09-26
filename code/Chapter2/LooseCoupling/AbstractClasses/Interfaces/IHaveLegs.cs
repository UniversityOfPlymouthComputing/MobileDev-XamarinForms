namespace AbstractClasses
{
    public interface IHaveLegs
    {
        uint LandSpeedIndex { get; }
        uint NumberOfLegs { get; }
        void Run();
    }
}