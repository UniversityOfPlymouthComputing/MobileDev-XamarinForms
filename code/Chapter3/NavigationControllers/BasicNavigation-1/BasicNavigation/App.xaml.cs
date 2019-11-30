using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasicNavigation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Instantiate the first Page in the hierarchy.
            MainPage firstPage = new MainPage();
            
            // Instantiate a NavigationPage which maintains a stack of pages
            // passing in the first page to be displayed (bottom of the stack)
            this.MainPage = new NavigationPage(firstPage);

            // The NavigationPage is now the MainPage of the Application.
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
