using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventsAndBindings
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private int _sliderValue = 1;
        public int SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                if (_sliderValue == value) return;
                _sliderValue = value;
                OnPropertyChanged();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            /*
            TextLabel.BindingContext = this.MySlider;
            TextLabel.SetBinding(Label.TextProperty, "Value");
            */
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int v = new Random().Next(1, 11);
            SliderValue = v;
        }
    }
}
