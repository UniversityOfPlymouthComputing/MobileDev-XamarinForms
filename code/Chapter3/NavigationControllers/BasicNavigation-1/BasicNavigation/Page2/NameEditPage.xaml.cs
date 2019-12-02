using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class NameEditPage : ContentPage
    {
        private string _name;

        // **************************** Accessors *****************************
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                NameEntry.Text = _name;
            }
        }

        // **************************** Constructor ****************************
        public NameEditPage(string name)
        {
            InitializeComponent();
            Name = name;
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
            Name = e.NewTextValue;
        }
    }
}
