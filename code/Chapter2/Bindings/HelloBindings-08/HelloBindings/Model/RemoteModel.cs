using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using ProfoundSayings;
using HelloBindingsLib;

namespace HelloBindings
{
    public class RemoteModel : ISayingsModel
    {
        //This model generates events for any other object to observe and react to
        public event PropertyChangedEventHandler PropertyChanged;
        //The number of strings in the collection
        protected int Count { get; set; } = 0;
        //URL string for the remote server
        protected const string Url = "https://sayingsfunctionappplymouth.azurewebsites.net/api/LookupSaying?index=";
        //Dynamically allocated HTTP client for performing a network connection
        protected static HttpClient _client;
        protected static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Add("x-functions-key", "Z8W37szNxA5mdRmDkblGr/3fimj3IPojd6l9tDTBo4pgyHRtklovAA==");
                }
                return _client;
            }
        }

        //Set true if valid data has been acquired
        protected bool _hasData = false;
        public bool HasData {
            get => _hasData;
            protected set
            {
                if (_hasData != value)
                {
                    _hasData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
                }
            }
         }

        //The position of the saying in the list
        protected int _sayingNumber = 0;
        public int SayingNumber
        {
            get => _sayingNumber;
            protected set
            {
                if (value != _sayingNumber)
                {
                    _sayingNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
                }
            }
        }

        //The currently fetched saying
        protected string _currentSaying = "Ye Olde Wise Sayings";
        public string CurrentSaying
        {
            get => _currentSaying;
            protected set
            {
                if (!_currentSaying.Equals(value))
                {
                    _currentSaying = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
                }
            }
        }

        //Set true while waiting on a network transaction
        protected bool _isRequestingFromNetwork = false;
        public bool IsRequestingFromNetwork
        {
            get => _isRequestingFromNetwork;
            protected set
            {
                if (value != _isRequestingFromNetwork)
                {
                    _isRequestingFromNetwork = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRequestingFromNetwork)));
                }
            }
        }
 
        //Fetches next saying from the network
        public async Task<(bool success, string ErrorString)> NextSaying()
        {
            //Perform fetch from a network
            int n = SayingNumber;
            n = HasData ? (n + 1) % Count : 0;
            IsRequestingFromNetwork = true;
            (bool success, string ErrStr) = await FetchSayingAsync(n);
            IsRequestingFromNetwork = false;
            return (success, ErrStr);
        }

        //Specific implmentation for fetching a saying from the network (Azure)
        protected virtual async Task<(bool success, string status)> FetchSayingAsync(int WithIndex = 0)
        {
            try
            {
                string result = await Client.GetStringAsync(Url + WithIndex);
                PayLoad p = PayLoad.FromXML(result);
                if (p != null)
                {
                    Count = p.From;
                    CurrentSaying = p.Saying;
                    SayingNumber = WithIndex;
                    HasData = true;
                    return (success: HasData, status: "OK");
                }
                else
                {
                    HasData = false;
                    return (success: HasData, status: "Invalid Response");
                }
            }
            catch
            {
                HasData = false;
                return (success: HasData, status: "Permission Denied");
            }

        }
    }
}
