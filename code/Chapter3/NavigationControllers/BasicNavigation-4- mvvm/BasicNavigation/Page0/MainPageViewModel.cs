using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class MainPageViewModel
    {
        public ICommand ButtonCommand { get; set; }
        private IMainPage View { get; set; }

        public MainPageViewModel(IMainPage view)
        {
            // IoC - view is not instantiated here, but elsewhere
            // In principle, this can be mocked for unit-testing purposes
            View = view;
            Console.WriteLine("Constructor for MainPageViewModel");

            //The command property - bound to a button in the view
            ButtonCommand = new Command(execute: NavigateToAboutPage_v2);
        }

        void NavigateToAboutPage_v1()
        {
            //This has a concrete reference to a view inside a VM - is this good/bad/indifferent?
            AboutPage about = new AboutPage("NickO v1");
            View.Navigation.PushAsync(about);
        }

        void NavigateToAboutPage_v2()
        {
            //This delegtes the navigation to the view and avoids references to
            //concrete view types in the ViewModel
            View.NavigateToAboutPageAsync("NickO v2");
        }

        // WHAT IS NOT DONE or SHOWN
        // Consider dependency injection to perform VM-first navigation
        // This is complex but does all the wiring / instantiation for you.
        // Suggest a 3rd party MVVM framework e.g. MVVMCross or Prism to name just two
    }
}
