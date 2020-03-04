using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        private ICallBackWithString presenter;
        private Action<string> callback;

        private string _stringData = "Second Page Data";
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

        public SecondPage(string TitleString, ICallBackWithString pres = null) 
        {
            InitializeComponent();
            this.presenter = pres;
            StringData = TitleString;
        }
        public SecondPage(string TitleString, Action<string> cb)
        {
            InitializeComponent();
            this.callback = cb;
            StringData = TitleString;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            StringData = "May the force me with you";
            //This has NO effect on the property in the previous page
            presenter?.UpdateString(StringData);
            callback?.Invoke(StringData);
            MessagingCenter.Send<SecondPage, string>(this, "StringUpdate", StringData);
            //_ = Navigation.PopAsync();
        }
    }
}