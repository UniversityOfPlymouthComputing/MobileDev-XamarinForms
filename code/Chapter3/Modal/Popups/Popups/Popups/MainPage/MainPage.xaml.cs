using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Popups
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, IMainPageHelper
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this);
        }

        INavigation IPage.NavigationProxy { get => Navigation; }

        public async Task<string> AskForString(string questionTitle, string question)
        {
            string result = await DisplayPromptAsync(questionTitle, question);
            return result;
        }

        public async Task<bool> YesNoAlert(string title, string message)
        {
            bool answer = await DisplayAlert(title, message, "Yes", "No");
            return answer;
        }
    }
}
