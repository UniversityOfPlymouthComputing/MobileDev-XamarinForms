using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NavDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _stringData = "Default Data";
        public string StringData
        {
            get => _stringData;
            set
            {
                if (_stringData == value) return;
                _stringData = value;
                OnPropertyChanged(nameof(StringData));
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        

        private void Button_Clicked(object sender, EventArgs e)
        {
            var nextPage = new SecondPage(ref _stringData);
            _ = Navigation.PushAsync(nextPage);
        }
    }
}
