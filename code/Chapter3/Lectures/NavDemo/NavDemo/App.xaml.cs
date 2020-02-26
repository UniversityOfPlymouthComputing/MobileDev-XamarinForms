using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage mainPage = new MainPage();
            NavigationPage rootPage = new NavigationPage(mainPage);
            MainPage = rootPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
