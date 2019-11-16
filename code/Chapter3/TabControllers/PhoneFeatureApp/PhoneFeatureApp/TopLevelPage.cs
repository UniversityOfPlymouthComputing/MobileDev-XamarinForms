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
            Children.Add(new LocationPage());
            Children.Add(new MotionPage());
        }
    }
}

