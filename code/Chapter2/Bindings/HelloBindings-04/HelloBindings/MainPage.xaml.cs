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
        Model DataModel = new Model();
      
        public MainPage()
        {
            InitializeComponent();

            ToggleSwitch.BindingContext = DataModel;
            MessageButton.BindingContext = DataModel;
            MessageLabel.BindingContext = DataModel;

            ToggleSwitch.SetBinding(Switch.IsToggledProperty, "IsTrue", BindingMode.OneWayToSource);

            MessageButton.SetBinding(Button.IsEnabledProperty, "IsTrue", BindingMode.OneWay);
            MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying {0:d}");

            MessageLabel.SetBinding(Label.TextProperty, "CurrentSaying", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.IsVisibleProperty, "IsTrue", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.TextColorProperty, "SayingNumber", BindingMode.OneWay, new ColorConverter());

        }

        private void MessageButton_Clicked(object sender, EventArgs e)
        {
            DataModel.NextMessage();
        }
    }
}
