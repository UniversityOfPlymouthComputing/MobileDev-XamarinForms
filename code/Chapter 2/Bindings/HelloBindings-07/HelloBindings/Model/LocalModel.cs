using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ProfoundSayings;
using HelloBindingsLib;

namespace HelloBindings
{ 
    class LocalModel : ISayingsModel 
    {
        // Don't need this - for now
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };

        //Index of which saying to use
        private int _sayingNumber = 0;
        public int SayingNumber
        {
            get => _sayingNumber;

            private set
            {
                if (value != _sayingNumber)
                {
                    _sayingNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
                }
            }
        }
        public string CurrentSaying
        {
            get => Sayings[SayingNumber];
        }
        private void NextSaying()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
        }
        public async Task NextSayingAsync()
        { 
            NextSaying();
        }
    }
}
