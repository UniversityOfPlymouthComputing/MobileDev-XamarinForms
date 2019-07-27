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

        public async Task ShowErrorMessageAsync(string ErrorMessage)
        {
            await DisplayAlert("Error", ErrorMessage, "OK");
        }

        public Task ShowModalAboutPageAsync()
        {
            //TODO:
            throw new NotImplementedException();
        }
    }
}
