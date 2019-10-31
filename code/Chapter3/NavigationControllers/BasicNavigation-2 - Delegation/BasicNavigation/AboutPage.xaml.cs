using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BasicNavigation
{
    public partial class AboutPage : ContentPage, IAboutAbout
    {
        private int year = 2020;
        public int Year
        {
            get => year;
            set
            {
                if (value != year)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }

        public AboutPage(string AuthorName)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Prev");
            LabelAuthor.Text = AuthorName;
        }

        public void YearWasUpdated(int year)
        {
            Year = year;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("OnAppearing");
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Console.WriteLine("OnDisappearing");
        }

        private async void ButtonMore_Clicked(object sender, EventArgs e)
        {
            //Pass a reference to self (to facilitate callbacks) and the current value
            AboutAbout more = new AboutAbout(this, Year);
            await Navigation.PushAsync(more, true);
        }
    }
}
