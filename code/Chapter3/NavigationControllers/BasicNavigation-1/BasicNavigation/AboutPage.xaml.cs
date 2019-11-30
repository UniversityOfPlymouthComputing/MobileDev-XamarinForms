using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BasicNavigation
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage(string AuthorName)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Prev");
            LabelAuthor.Text = AuthorName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("OnAppearing - do not count on this!");
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Console.WriteLine("OnDisappearing - do not count on this!");
        }

        private async void ButtonMore_Clicked(object sender, EventArgs e)
        {
            AboutAbout more = new AboutAbout();
            await Navigation.PushAsync(more, true);
        }
    }
}
