using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BasicNavigation
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //NavigationPage.SetBackButtonTitle(this, "Top");
        }

        private async void ButtonAbout_Clicked(object sender, EventArgs e)
        {
            //Push about page on the navigation stack with animation.
            AboutPage about = new AboutPage("NickO");
            await Navigation.PushAsync(about,true);
        }
    }
}
