using System.Collections.ObjectModel;


// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    //A collection with a few added properties
    public class PlanetGroup : ObservableCollection<SolPlanet>
    {
        public string GroupTitle { get; private set; }
        public string GroupShortName { get; private set; }
        public PlanetGroup(string title, string shortname)
        {
            GroupTitle = title;
            GroupShortName = shortname;
        }
    }
} //END OF NAMESPACE
