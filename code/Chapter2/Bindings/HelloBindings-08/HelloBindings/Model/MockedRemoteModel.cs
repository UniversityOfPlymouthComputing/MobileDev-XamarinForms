using HelloBindingsLib;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloBindings
{
    public class MockedRemoteModel : SayingsAbstractModel
    {
        private static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo",
            "Make it So!",
            "never show this",
        };

        private byte errorMode = 0;

        protected override async Task<PayLoad> FetchPayloadAsync(int WithIndex = 0)
        {
            await Task.Delay(1000);

            //Every last one simulate a network error or invalid response (unable to parse XML)
            if (WithIndex == Sayings.Count - 1)
            {
                if ((errorMode++) % 2 == 1)
                {
                    return null;
                }
                else
                {
                    throw new HttpRequestException();
                }
            }

            PayLoad p = new PayLoad
            {
                Saying = Sayings[WithIndex],
                From = Sayings.Count
            };
            return p;
        }
    }
}
