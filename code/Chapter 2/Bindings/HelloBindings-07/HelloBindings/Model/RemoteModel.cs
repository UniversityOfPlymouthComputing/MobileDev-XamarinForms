using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using ProfoundSayings;

namespace HelloBindings
{
    public class RemoteModel : ISayingsModel
    {
        private const string Url = "https://functionapphellobindings.azurewebsites.net/api/GetQuote";
        private HttpClient _client;

        private HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }

                return _client;
            }
        }
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
                }
            }
        }
        string _currentSaying = "Live Long and prosper";
        public string CurrentSaying
        {
            get {
                return _currentSaying;
            }
            private set {
                if (!value.Equals(_currentSaying))
                {
                    _currentSaying = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
                }
            }
        }

        private void NextSaying()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
        }
        public async Task NextSayingAsync()
        {
            //Simulate fetch from a network
            await Task.Delay(1000);
            string result = await Client.GetStringAsync(Url);
            CurrentSaying = result;
            NextSaying();

            //Read network - https://functionapphellobindings.azurewebsites.net/api/GetQuote

        }


    }
}
