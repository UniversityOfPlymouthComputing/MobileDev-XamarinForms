using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{
    //INotifyPropertyChanged  is already implemented for a ContentPage
    public partial class NameEditPage : ContentPage
    {
        public NameEditPage(NameEditPageViewModel vm)
        {
            InitializeComponent();

            //Bind to ViewModel
            BindingContext = vm;
        }
    }
}
