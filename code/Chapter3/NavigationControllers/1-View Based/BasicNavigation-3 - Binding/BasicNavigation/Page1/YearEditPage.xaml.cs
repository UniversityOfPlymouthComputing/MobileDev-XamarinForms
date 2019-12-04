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
            // Pass binding context forward and push page on navigation stack
            var nextPage = new NameEditPage();
            nextPage.BindingContext = this.BindingContext;
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
