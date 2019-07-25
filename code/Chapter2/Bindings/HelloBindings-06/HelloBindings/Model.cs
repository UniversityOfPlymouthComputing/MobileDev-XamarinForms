using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace HelloBindings
{
    class Model //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;


        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };

        //Index of which saying to use
        public int SayingNumber { get; private set; }
        public string CurrentSaying { get; private set;  }
        public void NextMessage()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
            CurrentSaying = Sayings[SayingNumber];
        }

        public Model()
        {
            CurrentSaying = Sayings[0];
        }
    }
}
