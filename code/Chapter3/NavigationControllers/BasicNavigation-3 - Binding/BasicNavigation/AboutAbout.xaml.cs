using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class AboutAbout : ContentPage
    {
        //Note how the reverse reference in passed as a parameter
        public AboutAbout()
        {
            InitializeComponent();
        }

        private async void DoNavigateTop(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
