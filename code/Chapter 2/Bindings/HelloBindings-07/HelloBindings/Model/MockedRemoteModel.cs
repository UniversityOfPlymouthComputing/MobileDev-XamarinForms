using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloBindings
{
    public class MockedRemoteModel : RemoteModel
    {
        //Override method that performs the network transaction with a locally mocked version
        protected override async Task<bool> FetchSayingAsync(int WithIndex = 0)
        {
            //Local memory based backend store
            List<string> LocalSayings = new List<string>
            {
                "May the Force be With You",
                "Live long and prosper",
                "Nanoo nanoo"
            };
            //Simulate network latency
            await Task.Delay(1000);

            //Simulate setting the result
            Count = LocalSayings.Count;
            CurrentSaying = LocalSayings[WithIndex];
            SayingNumber = WithIndex;
            HasData = true;
            return HasData;
        }
    }
}
