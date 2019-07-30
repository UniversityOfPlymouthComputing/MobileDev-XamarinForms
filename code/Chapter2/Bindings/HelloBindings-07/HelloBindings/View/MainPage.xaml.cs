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
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(new RemoteModel());

            //BindingContext = new MainPageViewModel();

            //Bindings
            //ToggleSwitch.BindingContext = ViewModel;
            //ToggleSwitch.SetBinding(Switch.IsToggledProperty, "UIVisible", BindingMode.OneWayToSource);

            //MessageButton.BindingContext = ViewModel;
            //MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying: {0:d}");
            //MessageButton.SetBinding(Button.CommandProperty, "FetchNextSayingCommand"); //Cannot work as Model has no such (Forms aware) property

            //MessageLabel.BindingContext = ViewModel;
            //MessageLabel.SetBinding(Label.TextProperty, "CurrentSaying", BindingMode.OneWay);
            //MessageLabel.SetBinding(Label.IsVisibleProperty, "UIVisible", BindingMode.OneWay);
            //MessageLabel.SetBinding(Label.TextColorProperty, "SayingNumber", BindingMode.OneWay, new ColorConverter());

        }

    }
}
