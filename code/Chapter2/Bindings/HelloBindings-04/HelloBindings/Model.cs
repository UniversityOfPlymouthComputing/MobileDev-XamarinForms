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

        //Index of which saying to use
        private int next = 0;
        public int SayingNumber
        {
            get => next;
            set
            {
                if (next != value)
                {
                    next = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SayingNumber"));
                    }
                }
            }
        }
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
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
            CurrentSaying = Sayings[SayingNumber];
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
            _currentSaying = Sayings[0];
        }
    }
}
