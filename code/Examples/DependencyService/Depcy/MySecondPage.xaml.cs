using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Depcy
{
    public partial class MySecondPage : ContentPage
    {
        public MySecondPage()
        {
            InitializeComponent();
            TheButton.Clicked += TheButton_Clicked;
            GoAwayButton.Clicked += GoAwayButton_Clicked;
            TheModalButton.Clicked += TheModalButton_Clicked;
        }

        private void TheModalButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new AboutPage());
        }

        private async void GoAwayButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void TheButton_Clicked(object sender, EventArgs e)
        {
            IRobot r = DependencyService.Get<IRobot>();
            int d = r.WalkForward(10);
            Console.WriteLine($"{d} steps");
        }
    }
}
