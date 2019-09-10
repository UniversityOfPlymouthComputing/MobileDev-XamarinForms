using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            TabbedPage t = new TabbedPage();
            t.Children.Add(new MainPage());
            t.Children.Add(new Page1());
            t.Children.Add(new Page2());
            MainPage = t;
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
