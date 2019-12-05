using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
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
            await Navigation.PopToRootAsync();  //Back to root view (bottom of stack)
            //await Navigation.PopAsync();      //Back to previous page
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Name = e.NewTextValue;
            Console.WriteLine(Name);
        }
    }
}
