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
        public int SayingNumber { get; private set; }
        public string Message { get; private set;  }
        public void NextMessage()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
            Message = Sayings[SayingNumber];
        }

        public Model()
        {
            Message = Sayings[0];
        }
    }
}
