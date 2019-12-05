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

            //This next page does non-destructive editing - back button title reflects this
            NavigationPage.SetBackButtonTitle(this, "Cancel");

            //Events
            EditButton.Clicked += EditButton_Clicked;
        }

        // **************************** Event handlers *****************************
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var nextPage = new NameEditPage();
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
