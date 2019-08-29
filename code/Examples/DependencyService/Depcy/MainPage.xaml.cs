using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Depcy
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DoButton.Clicked += DoButton_Clicked;
        }

        private async void DoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MySecondPage());
        }
    }
}
