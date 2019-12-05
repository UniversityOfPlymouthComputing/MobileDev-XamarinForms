using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BasicNavigation
{
    public partial class YearEditPage : ContentPage
    {
        // ********************* Constructor *********************
        public YearEditPage()
        {
            InitializeComponent();

            //This next page will do non-destructive editing - back button title reflects this
            NavigationPage.SetBackButtonTitle(this, "Cancel");

            //Attach Event Handlers
            EditButton.Clicked += EditButton_Clicked;
            YearSlider.ValueChanged += YearSlider_ValueChanged;
        }

        // **************************** Event handlers *****************************
        private void YearSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var Year = (int)e.NewValue;
            Console.WriteLine(Year);
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var nextPage = new NameEditPage();
            await Navigation.PushAsync(nextPage, true);
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
    }
}
