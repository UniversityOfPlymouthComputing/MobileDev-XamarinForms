using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Commanding
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
            BindingContext = new SecondViewModel(Navigation);
        }
    }
}
