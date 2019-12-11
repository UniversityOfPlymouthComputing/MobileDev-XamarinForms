using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class NameEditPage : ContentPage
    {
        // **************************** Constructor ****************************
        public NameEditPage()
        {
            InitializeComponent();
            SaveButton.Clicked += SaveButton_Clicked;
        }

        // ************************** Event Handlers ***************************
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Save Clicked");
            await Navigation.PopToRootAsync();
        }
    }
}
