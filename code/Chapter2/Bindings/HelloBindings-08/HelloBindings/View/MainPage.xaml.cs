using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HelloBindings
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, IMainPageViewHelper
    {
        private MainPageViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            //Create ViewModel
            ViewModel = new MainPageViewModel(new RemoteModel(), this);
            BindingContext = ViewModel;
        }

        public ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute)
        {
            return new Command(execute, canExecute);
        }

        public void ChangeCanExecute(ICommand cmd)
        {
            Command c = cmd as Command;
            c.ChangeCanExecute();
        }

        //Show error message dialog
        public async Task ShowErrorMessageAsync(string ErrorMessage)
        {
            await DisplayAlert("Error", ErrorMessage, "OK");
        }

        //Show modal AboutPage (could be called from the ViewModel)
        public async Task ShowModalAboutPageAsync()
        {
            var about = new AboutPage();
            await Navigation.PushModalAsync(about);
        }

        //View-specific event handler
        private async void DoAboutButtonClicked(object sender, EventArgs e)
        {
            await ShowModalAboutPageAsync();
        }
    }
}
