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
        //Data
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
            
            MessageLabel.BindingContext = ToggleSwitch;
            MessageLabel.SetBinding(Label.IsVisibleProperty, "IsToggled", BindingMode.TwoWay);
            MessageButton.BindingContext = ToggleSwitch;
            MessageButton.SetBinding(Button.IsEnabledProperty, "IsToggled", BindingMode.TwoWay);
        }



        private void MessageButton_Clicked(object sender, EventArgs e)
        {
            MessageLabel.Text = Sayings[next];
            next = (next + 1) % Sayings.Count;

            //Sneaky trick
            if (next == 0)
            {
                MessageLabel.IsVisible = false;
            }
        }
    }
}
