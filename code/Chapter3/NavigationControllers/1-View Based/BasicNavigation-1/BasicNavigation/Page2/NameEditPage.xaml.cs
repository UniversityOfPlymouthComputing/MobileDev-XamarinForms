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

            //Attach event handlers
            NameEntry.TextChanged += NameEntry_TextChanged;
            SaveButton.Clicked += SaveButton_Clicked;
        }

        // ************************** Event Handlers ***************************
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Save Clicked");
            await Navigation.PopToRootAsync();
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Name = e.NewTextValue;
            Console.WriteLine(Name);
        }
    }
}
