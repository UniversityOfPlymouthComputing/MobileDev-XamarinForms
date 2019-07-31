using System.Net.Http;
using System.Threading.Tasks;
using HelloBindingsLib;

namespace HelloBindings
{
    public class RemoteModel : SayingsAbstractModel
    {
        // URL string for the remote server

        //protected const string Url = "https://sayingsfunctionappplymouth.azurewebsites.net/api/LookupSaying";
        protected const string Url = "http://10.0.2.2:7071/api/LookupSaying"; // 10.0.2.2 is mapped through to the host PC

        // Azure security key (using Function level authentication - now out of date ;)
        protected const string azure_fn_key = "Z8W37szNxA5mdRmDkblGr/3fimj3IPojd6l9tDTBo4pgyHRtklovAA==";

        //Dynamically allocated HTTP client for performing a network connection
        protected static HttpClient _client;
        protected static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Add("x-functions-key", azure_fn_key);
                }
                return _client;
            }
        }

        protected override async Task<PayLoad> FetchPayloadAsync(int WithIndex = 0)
        {
            string result = await Client.GetStringAsync($"{Url}?index={WithIndex}");
            PayLoad p = PayLoad.FromXML(result);
            return p;
        }
    }
}
