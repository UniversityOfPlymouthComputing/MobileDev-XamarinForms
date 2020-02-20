// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleTableView
{
    public class SolPlanet
    {
        public string Name { get; set; } = "Earth";
        public double Distance { get; set; } = 147.0;
        public bool Explored { get; set; } = true;
        public SolPlanet(string name, double dist, bool explored = false) => (Name, Distance, Explored) = (name, dist, explored);
        public SolPlanet(SolPlanet p) => Copy(p);

        //Copy element by element
        public void Copy(SolPlanet p)
        {
            Name = p.Name;
            Distance = p.Distance;
            Explored = p.Explored;
        }

    }
} //END OF NAMESPACE
