using System;
using PhoneFeatureApp.DeviceInfo;
using PhoneFeatureApp.Location;
using PhoneFeatureApp.Motion;
using Xamarin.Forms;

namespace PhoneFeatureApp
{
    public class TopLevelPage : TabbedPage
    {
        public TopLevelPage()
        {
            Title = "Phone Feature App";
            Children.Add(new DeviceInfoPage());
            var loc = new NavigationPage(new LocationPage());
            loc.Title = "Location";
            loc.IconImageSource = "location.png";
            Children.Add(loc);
            Children.Add(new MotionPage());
            if (Device.RuntimePlatform == Device.Android)
            {
                //BarBackgroundColor = new Color(1.0, 1.0, 1.0);
                //BarTextColor = new Color(0.0, 0.0, 1.0);

                //Try this instead of the above two lines
                Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            }
        }
    }
}

