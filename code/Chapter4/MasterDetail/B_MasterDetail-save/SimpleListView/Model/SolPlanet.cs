// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

using System;
using System.Runtime.CompilerServices;
using uoplib.mvvm;

namespace SimpleListView
{
    public class SolPlanet : BindableModelBase
    {
        private string _name = "Earth";
        public string Name {
            get => _name;
            set => Update<string>(ref _name, value);
        }
        private double _distance = 147.0;
        public double Distance {
            get => _distance;
            set => Update<double>(ref _distance, value);
        }

        private bool _explored = true;
        public bool Explored {
            get => _explored;
            set => Update<bool>(ref _explored, value);
        }

        public SolPlanet(string name, double dist, bool explored = false) => (Name, Distance, Explored) = (name, dist, explored);
        public SolPlanet(SolPlanet p) => Copy(p);

        //Copy element by element
        public void Copy(SolPlanet p)
        {
            Name = p.Name;
            Distance = p.Distance;
            Explored = p.Explored;
        }

        //Change explored category
        public void ToggleExplored() => Explored = !Explored;

    }
} //END OF NAMESPACE
