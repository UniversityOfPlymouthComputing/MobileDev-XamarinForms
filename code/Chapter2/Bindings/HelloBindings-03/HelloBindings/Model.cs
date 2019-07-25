using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace HelloBindings
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };

        private int next = 0;

        string _currentSaying = "Welcome to Xamarin Forms!";
        public string CurrentSaying
        {
            get => _currentSaying;
            set
            {
                if (!value.Equals(_currentSaying))
                {
                    _currentSaying = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentSaying"));
                    }
                }
            }
        }

        public void NextMessage()
        {
            CurrentSaying = Sayings[next];
            next = (next+ 1) % Sayings.Count;
        }

        bool _visible = true;
        public bool IsTrue
        {
            get => _visible;
            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsTrue"));
                    }
                }
            }
        }


        public Model()
        {

        }
    }
}
