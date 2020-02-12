using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BindingsLecture
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _anotherTitle = "This is Label 2";
        public string AnotherTitle
        {
            get => _anotherTitle;
            set
            {
                if (_anotherTitle == value) return;
                _anotherTitle = value;
                OnPropertyChanged("AnotherTitle");
            }
        }



        public MainViewModel ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.BindingContext = ViewModel;

            //Set the source object on the target
            Label2.BindingContext = this;
            //Apply binding to target
            Label2.SetBinding(Label.TextProperty, "AnotherTitle");

            //Again, variant 2
            var binding = new Binding(source: MySlider, path: "Value", stringFormat: "Slider Value: {0:F0}");
            Label4.SetBinding(Label.TextProperty, binding);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            //This has no effect
            MyXamarinConstants.BackgroundCol = new Color(1.0, 0.0, 0.0);

            //This does have an effect
            TitleLabel.Text = "Button 1 Tapped";
        }

        void Button2_Clicked(System.Object sender, System.EventArgs e)
        {
            //Simply set a property - and see the effect
            AnotherTitle = "Button 2 Tapped";
        }

        void Button3_Clicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.Greeting = "Button 3 Tapped";
        }
    }
}
