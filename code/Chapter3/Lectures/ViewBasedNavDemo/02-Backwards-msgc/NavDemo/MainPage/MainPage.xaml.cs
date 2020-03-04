using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NavDemo
{
    public interface ICallBackWithString
    {
        void UpdateString(string str);
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, ICallBackWithString
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
            // Method 1 - pass reference to self
            SecondPage nextPage = new SecondPage(_stringData, this);
            
            // Method 2 - pass in a closure
            //SecondPage nextPage = new SecondPage(_stringData, (string s) =>
            //{
            //    StringData = s;
            //});

            // Method 3 - MessageCenter
            //MessagingCenter.Subscribe<SecondPage, string>(this, "StringUpdate", UpdateString);
            //SecondPage nextPage = new SecondPage(_stringData);

            _ = Navigation.PushAsync(nextPage);
        }

        public void UpdateString(string str)
        {
            StringData = str;
        }
        public void UpdateString(object sender, string str)
        {
            StringData = str;
        }
    }
}
