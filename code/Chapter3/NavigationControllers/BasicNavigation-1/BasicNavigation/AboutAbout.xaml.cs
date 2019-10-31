using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BasicNavigation
{
    public partial class AboutAbout : ContentPage
    {
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
