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
        MainPageViewModel ViewModel = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();

            //BindingContext is the same for all elements, so can use the containing view
            
            //ToggleSwitch.BindingContext = ViewModel;
            //MessageButton.BindingContext = ViewModel;
            //MessageLabel.BindingContext = ViewModel;
            BindingContext = ViewModel;

            ToggleSwitch.SetBinding(Switch.IsToggledProperty, "UIVisible", BindingMode.OneWayToSource);
            MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying: {0:d}");
            MessageButton.SetBinding(Button.CommandProperty, "ButtonCommand"); 

            MessageLabel.SetBinding(Label.TextProperty, "CurrentSaying", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.IsVisibleProperty, "UIVisible", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.TextColorProperty, "SayingNumber", BindingMode.OneWay, new ColorConverter());
        }
    }
}
