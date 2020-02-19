namespace Commanding
{
    public class StarFleetStaff
    {
        public StarFleetStaff(string title, string uniformColour)
        {
            Title = title;
            UniformColour = uniformColour;
        }

        public string Title { get; set; }
        public string UniformColour { get; set; }
    }
}
