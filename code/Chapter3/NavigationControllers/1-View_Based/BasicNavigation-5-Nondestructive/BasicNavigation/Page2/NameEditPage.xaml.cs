using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class NameEditPage : ContentPage
    {
        //Local property bound to UI
        private string _name = "Anon";
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        // **************************** Constructor ****************************
        public NameEditPage()
        {
            InitializeComponent();

            //Update local property with copy of global value
            this.Name = SingletonModel.SharedInstance.Name;

            SaveButton.Clicked += SaveButton_Clicked;
        }

        // ************************** Event Handlers ***************************
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Save Clicked");

            //Commit changes to global model
            SingletonModel.SharedInstance.Name = this.Name;

            //Navigation back
            await Navigation.PopToRootAsync();
        }
    }
}
