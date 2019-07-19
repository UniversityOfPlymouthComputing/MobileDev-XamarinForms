using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HelloBindings
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Model DataModel = new Model();

        public ICommand ButtonCommand
        {
            get; private set;
        }

        public MainPage()
        {
            InitializeComponent();

            ButtonCommand = new Command( execute: () => DataModel.NextMessage());

            //Bindings
            ToggleSwitch.BindingContext = DataModel;
            ToggleSwitch.SetBinding(Switch.IsToggledProperty, "IsTrue", BindingMode.OneWayToSource);

            MessageButton.BindingContext = DataModel;
            MessageButton.SetBinding(Button.IsEnabledProperty, "IsTrue", BindingMode.OneWay);
            MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying {0:d}");
            //MessageButton.SetBinding(Button.CommandProperty, "ButtonCommand"); //Cannot work as Model has no such (Forms aware) property

            MessageLabel.BindingContext = DataModel;
            MessageLabel.SetBinding(Label.TextProperty, "Message", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.IsVisibleProperty, "IsTrue", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.TextColorProperty, "SayingNumber", BindingMode.OneWay, new ColorConverter());

        }

        private void MessageButton_Clicked(object sender, EventArgs e)
        {
            DataModel.NextMessage();
        }
    }
}
