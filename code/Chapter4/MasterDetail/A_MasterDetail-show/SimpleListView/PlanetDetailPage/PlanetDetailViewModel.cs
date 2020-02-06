using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using uoplib.mvvm;

namespace SimpleListView
{
    public class PlanetDetailViewModel : ViewModelBase
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

        //Overwrite the original
        private void Save() => _original.Copy(_model);

        public PlanetDetailViewModel() : base(null) => throw new Exception("Parameterless constructor not supported");

        public PlanetDetailViewModel(SolPlanet p) : base(null)
        {
            //Keep a reference to the original
            _original = p;

            //Make an indepednent copy
            _model = new SolPlanet(p);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);
        }

    }
}
