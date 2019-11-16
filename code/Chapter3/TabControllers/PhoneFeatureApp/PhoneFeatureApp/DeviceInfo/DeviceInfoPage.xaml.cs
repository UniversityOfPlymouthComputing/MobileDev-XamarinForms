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
            BindingContext = new DeviceInfoViewModel();
            this.IconImageSource = "device.png";
        }
    }
}
