using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class AboutAbout : ContentPage
    {
        public AboutAbout(AboutAboutViewModel vm)
        {
            InitializeComponent();

            //Bind to ViewModel
            BindingContext = vm;
        }
    }
}
