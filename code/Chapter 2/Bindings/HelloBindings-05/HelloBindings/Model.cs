using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

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
            get
            {
                return next;
            }
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
        string _message = "Welcome to Xamarin Forms!";
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (!value.Equals(_message))
                {
                    _message = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Message"));
                    }
                }
            }
        }

        public void NextMessage()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
            Message = Sayings[SayingNumber];
        }

        bool _visible = true;
        public bool IsTrue
        {
            get
            {
                return _visible;
            }
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
            _message = Sayings[0];
        }
    }
}
