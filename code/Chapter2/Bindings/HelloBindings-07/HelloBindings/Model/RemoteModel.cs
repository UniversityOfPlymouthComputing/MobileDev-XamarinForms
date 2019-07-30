using System.Net.Http;
using System.Threading.Tasks;
using HelloBindingsLib;

namespace HelloBindings
{
    public class RemoteModel : SayingsAbstractModel
    {

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

        protected override async Task<PayLoad> FetchPayloadAsync(int WithIndex = 0)
        {
            string result = await Client.GetStringAsync(Url + WithIndex);
            PayLoad p = PayLoad.FromXML(result);
            return p;
        }
    }
}
