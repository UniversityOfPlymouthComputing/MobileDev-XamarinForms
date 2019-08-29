using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Depcy
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BogOffButton.Clicked += BogOffButton_ClickedAsync;
            WarningButton.Clicked += WarningButton_Clicked;
        }

        private void WarningButton_Clicked(object sender, EventArgs e)
        {
            IRobot r = DependencyService.Get<IRobot>();
            int d = r.WalkForward(10);
            Console.WriteLine($"{d} steps");
        }

        private async void BogOffButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
