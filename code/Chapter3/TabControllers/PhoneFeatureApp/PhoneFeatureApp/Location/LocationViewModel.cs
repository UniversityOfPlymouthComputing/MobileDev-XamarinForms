using System;
using MVVMBase;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace PhoneFeatureApp.Location
{
    public class LocationViewModel : ViewModelCommon
    {
        public LocationViewModel()
        {
            subscribeToBackgroundColChange();
        }
    }
}
