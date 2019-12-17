using System;
using PhoneFeatureApp.DeviceInfo;
using PhoneFeatureApp.Location;
using PhoneFeatureApp.Motion;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneFeatureApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new TopLevelPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
