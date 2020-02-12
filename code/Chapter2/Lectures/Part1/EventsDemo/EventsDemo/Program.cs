namespace EventsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var objA = new ClassA();
            var objB = new ClassB(objA);
            objA.DeclarePi();
        }
    }
}
