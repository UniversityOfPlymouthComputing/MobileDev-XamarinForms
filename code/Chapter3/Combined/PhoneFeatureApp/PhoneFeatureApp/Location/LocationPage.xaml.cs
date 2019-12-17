using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PhoneFeatureApp.Location
{
    public partial class LocationPage : ContentPage
    {
        public static INavigation NavigationContainer;
        public LocationPage()
        {
            InitializeComponent();
            BindingContext = new LocationPageViewModel();
            this.IconImageSource = "location.png";
            NavigationContainer = this.Navigation;
        }
    }
}
