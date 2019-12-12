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

            //Instantiate the data model
            PersonDetailsModel m = new PersonDetailsModel("Anon");

            //Instantiate the viewmodel, and pass it a reference to the model
            FirstPageViewModel vm = new FirstPageViewModel(m);

            //Instantiatge the view, and pass it a reference to the viewmodel
            FirstPage firstPage = new FirstPage(vm);

            //Navigate in the first page
            MainPage = new NavigationPage(firstPage);
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
