using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class AboutAbout : ContentPage
    {
        private IAboutAbout Delegate {get; set;}

        private int year = 2020;

        public double Year {
            get => year;
            set {
                if ((int)value != year)
                {
                    year = (int)value;
                    OnPropertyChanged();
                    Delegate?.YearWasUpdated(year);
                }
            }
        }

        //Note how the reverse reference in passed as a parameter
        public AboutAbout(IAboutAbout d, int TheYear)
        {
            InitializeComponent();
            Delegate = d;
            Year = TheYear;
        }

        private async void DoNavigateTop(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
