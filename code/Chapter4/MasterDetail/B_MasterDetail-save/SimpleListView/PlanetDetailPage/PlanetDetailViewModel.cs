using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using uoplib.mvvm;
using Xamarin.Forms;

namespace SimpleListView
{
    public class PlanetDetailViewModel : ViewModelBase
    {
        private SolPlanet _original;
        public SolPlanet Model { get; set; }
        
        public ICommand SaveCommand
        {
            get; set;
        }

        //Overwrite the original
        private void Save() => _original.Copy(Model);

        public PlanetDetailViewModel() : base(null) => throw new Exception("Parameterless constructor not supported");

        public PlanetDetailViewModel(SolPlanet p, INavigation nav) : base(nav)
        {
            //Keep a reference to the original
            _original = p;

            //Make an indepednent copy
            Model = new SolPlanet(p);

            //Commands
            SaveCommand = new Command(execute: () =>
            {
                Save();
                _ = Navigation.PopAsync();
            });
        }
    }
}
