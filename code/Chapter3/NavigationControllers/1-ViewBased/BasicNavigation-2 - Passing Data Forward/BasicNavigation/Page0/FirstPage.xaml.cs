using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{

    [DesignTimeVisible(false)]
    public partial class FirstPage : ContentPage
    {
        private string _name;
        private int _year;

        // **************************** Accessors *****************************
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;

                _name = value;
                NameLabel.Text = _name;
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                if (_year == value) return;

                _year = value;
                YearLabel.Text = $"(c) {_year}";
            }
        }

        // **************************** Constructor *****************************
        public FirstPage(string name = "Anon")
        {
            InitializeComponent();
            EditButton.Clicked += EditButton_Clicked;
            Year = 2020;
            Name = name;
        }

        // **************************** Event handlers *****************************
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            //Pass data forward and push on navigation stack
            var nextPage = new YearEditPage(Name, Year);
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
