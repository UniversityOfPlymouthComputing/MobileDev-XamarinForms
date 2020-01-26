// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    public class SolPlanet
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public SolPlanet(string name, double dist) => (Name, Distance) = (name, dist);
    }
} //END OF NAMESPACE
