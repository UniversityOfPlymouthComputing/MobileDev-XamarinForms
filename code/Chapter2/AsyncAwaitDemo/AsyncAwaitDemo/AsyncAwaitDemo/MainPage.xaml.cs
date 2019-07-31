using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsyncAwaitDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            Activity1.IsRunning = true;
            await Task.Delay(3000);
            Activity1.IsRunning = false;
            ((Button)sender).IsEnabled = true;
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            Activity2.IsRunning = true;
            await Task.Delay(3000);
            Activity2.IsRunning = false;
            ((Button)sender).IsEnabled = true;
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            Activity3.IsRunning = true;
            await Task.Delay(3000);
            Activity3.IsRunning = false;
            ((Button)sender).IsEnabled = true;
        }
    }
}
