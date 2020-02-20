using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using uoplib.mvvm;
using Xamarin.Forms;

namespace SimpleTableView
{
    public class PlanetDetailsViewModel : ViewModelBase
    {
        private SolPlanet _original;
        private SolPlanet _model;
        
        public string PlanetName {
            get => _model.Name;
            set {
                if (value == _model.Name) return;
                _model.Name = value;
                OnPropertyChanged();
            }
        }

        public double DistanceFromSun
        {
            get => _model.Distance;
            set
            {
                if (value == _model.Distance) return;
                _model.Distance = value;
                OnPropertyChanged();
            }
        }

        public bool HasBeenExplored
        {
            get => _model.Explored;
            set
            {
                if (value == _model.Explored) return;
                _model.Explored = value;
                OnPropertyChanged();
            }
        }

        public ICommand DoubleTapCommand { get; set; }

        //Overwrite the original
        private void Save() => _original.Copy(_model);

        public PlanetDetailsViewModel() => throw new Exception("Parameterless constructor not supported");

        public PlanetDetailsViewModel(SolPlanet p)
        {
            //Keep a reference to the original
            _original = p;

            //Make an indepednent copy
            _model = new SolPlanet(p);

            //Commanding
            DoubleTapCommand = new Command(execute: () => {
                DistanceFromSun = 500.0;
            });
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);
        }

    }
}
