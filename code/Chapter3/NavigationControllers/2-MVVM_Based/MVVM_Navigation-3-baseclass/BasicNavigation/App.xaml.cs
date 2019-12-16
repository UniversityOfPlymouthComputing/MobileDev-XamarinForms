using System;
using Xamarin.Essentials;
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

            //Try and load from persistent storage
            string mainDir = FileSystem.AppDataDirectory;
            string path = System.IO.Path.Combine(mainDir, "userdetails.xml");
            PersonDetailsModel m = BindableModelBase.Load<PersonDetailsModel>(path);
            if (m == null)
            {
                //No such file, then create a new model with defaults and save
                m = new PersonDetailsModel("Anon");
                m.Save(path);
            }

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
