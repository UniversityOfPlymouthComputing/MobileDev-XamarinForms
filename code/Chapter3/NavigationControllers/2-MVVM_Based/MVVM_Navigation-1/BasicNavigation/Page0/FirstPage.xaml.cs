using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BasicNavigation
{

    [DesignTimeVisible(false)]
    public partial class FirstPage : ContentPage
    {
        public FirstPage(FirstPageViewModel vm = null)
        {
            InitializeComponent();
            BindingContext = vm ?? new FirstPageViewModel();
        }
    }
}
