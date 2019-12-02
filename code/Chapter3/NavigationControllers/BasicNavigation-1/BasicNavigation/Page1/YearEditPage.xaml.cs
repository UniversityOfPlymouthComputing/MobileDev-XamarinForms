using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BasicNavigation
{
    public partial class YearEditPage : ContentPage
    {
        private string _name;
        private int _year;

        // ******************* Accessors *******************
        public string Name {
            get => _name;
            set
            {
                if (_name == value) return;

                _name = value;
                NameLabel.Text = _name;
            }
        }
        public int Year {
            get => _year;
            set
            {
                if (_year == value) return;

                _year = value;
                YearSlider.Value = Convert.ToDouble(_year);
            }
        }

        // ********************* Constructor *********************
        public YearEditPage(string name = "Anon", int year = 1970)
        {
            InitializeComponent();

            //Populate UI
            Name = name;
            Year = year;

            //This next page does non-destructive editing - back button title reflects this
            NavigationPage.SetBackButtonTitle(this, "Cancel");

            //Events
            EditButton.Clicked += EditButton_Clicked;
            YearSlider.ValueChanged += YearSlider_ValueChanged;
        }

        // **************************** Event handlers *****************************
        private void YearSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Year = (int)e.NewValue;
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var nextPage = new NameEditPage(Name);
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
