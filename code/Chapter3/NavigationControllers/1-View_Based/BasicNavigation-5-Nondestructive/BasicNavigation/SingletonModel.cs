using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BasicNavigation
{
    public class SingletonModel : INotifyPropertyChanged
    {
        private static SingletonModel _model;
        private string _name = "Anon";
        private int _year = 2021;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        public int Year {
            get => _year;
            set
            {
                if (_year == value) return;
                _year = value;
                OnPropertyChanged();
            }
        }

        public static SingletonModel SharedInstance
        {
            get
            {
                if (_model == null)
                {
                    _model = new SingletonModel();
                }
                return _model;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
