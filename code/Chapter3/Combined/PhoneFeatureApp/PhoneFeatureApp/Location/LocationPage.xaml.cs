using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PhoneFeatureApp.Location
{
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
            BindingContext = new LocationPageViewModel();
            this.IconImageSource = "location.png";
        }
    }
}
