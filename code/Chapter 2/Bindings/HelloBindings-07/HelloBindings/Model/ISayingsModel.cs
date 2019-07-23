using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ProfoundSayings
{
    interface ISayingsModel : INotifyPropertyChanged
    {
        string CurrentSaying { get; }         // The current saying
        int SayingNumber { get; }             // Which saying this is in the sequence
        bool IsRequestingFromNetwork { get; } // True while a network transaction is taking place
        Task NextSaying();                    // Fetch the next saying from the backing store

        /* If you did not need to await, do this to satisfy the interface
        public Task NextSaying()
        {
            //Simulate fetch from a network
            return Task.CompletedTask;
        }
        */
    }
}
