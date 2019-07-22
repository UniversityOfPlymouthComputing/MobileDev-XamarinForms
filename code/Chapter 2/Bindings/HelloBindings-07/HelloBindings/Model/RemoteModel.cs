using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ProfoundSayings;

namespace HelloBindings
{
    public class RemoteModel : ISayingsModel
    {
        public RemoteModel()
        {
        }
        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };

        private int _sayingNumber = 0;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public string CurrentSaying => Sayings[SayingNumber];

        private void NextSaying()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
        }
        public async Task NextSayingAsync()
        {
            //Simulate fetch from a network
            await Task.Delay(1000);
            NextSaying();
        }


    }
}
