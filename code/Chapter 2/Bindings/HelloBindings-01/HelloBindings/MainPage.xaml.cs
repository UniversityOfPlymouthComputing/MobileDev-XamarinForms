using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloBindings
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };
        private int next = 0;
      
        public MainPage()
        {
            InitializeComponent();
        }

        private void ToggleSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            MessageLabel.IsVisible = ToggleSwitch.IsToggled;
            MessageButton.IsEnabled = ToggleSwitch.IsToggled;
        }

        private void MessageButton_Clicked(object sender, EventArgs e)
        {
            MessageLabel.Text = Sayings[next];
            next = (next + 1) % Sayings.Count;
        }
    }
}
