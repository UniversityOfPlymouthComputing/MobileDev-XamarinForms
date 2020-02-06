using System;
using System.Collections.Generic;
using uoplib.mvvm;
using Xamarin.Forms;

namespace SimpleListView
{
    public partial class PlanetDetailPage : ContentPage
    {
        public PlanetDetailPage(PlanetDetailViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
