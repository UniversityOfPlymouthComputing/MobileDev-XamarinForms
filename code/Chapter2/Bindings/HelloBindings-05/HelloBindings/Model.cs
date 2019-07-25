using System.Collections.Generic;
using System.ComponentModel;

namespace HelloBindings
{
    class Model : INotifyPropertyChanged
    {
        //Keeping this around - I will need it later ;)
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };

        public int SayingNumber { get; private set; } //Index of which saying to use
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
