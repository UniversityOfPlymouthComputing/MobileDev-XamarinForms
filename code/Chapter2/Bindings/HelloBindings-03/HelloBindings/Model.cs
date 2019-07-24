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
            Message = Sayings[next];
            next = (next+ 1) % Sayings.Count;
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

        }
    }
}
