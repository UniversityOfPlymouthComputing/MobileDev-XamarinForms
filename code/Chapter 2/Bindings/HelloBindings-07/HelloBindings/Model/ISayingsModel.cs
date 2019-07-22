using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ProfoundSayings
{
    interface ISayingsModel : INotifyPropertyChanged
    {
        string CurrentSaying { get; }
        int SayingNumber { get; }
        Task NextSayingAsync();
    }
}
