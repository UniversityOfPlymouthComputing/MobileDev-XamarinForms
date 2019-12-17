using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PhoneFeatureApp.DeviceInfo
{
    public partial class DeviceInfoPage : ContentPage
    {
        public DeviceInfoPage()
        {
            InitializeComponent();
            BindingContext = new DeviceInfoPageViewModel();

            //For this example, IconImageSource is set in XAML
            //this.IconImageSource = "device.png";
        }
    }
}
