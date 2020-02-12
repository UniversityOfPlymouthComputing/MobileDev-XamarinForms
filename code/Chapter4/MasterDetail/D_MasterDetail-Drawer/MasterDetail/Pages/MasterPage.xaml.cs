using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MasterDetail
{

    public partial class MasterPage : ContentPage
    {
        public event EventHandler<MasterPageEvent> PageSelected;

        public MasterPage()
        {
            InitializeComponent();
            Page_1_Cell.Tapped += (s, e) => PageSelected(this, MasterPageEvent.Show_Page_1);
            Page_2_Cell.Tapped += (s, e) => PageSelected(this, MasterPageEvent.Show_Page_2);
            ResetButton.Clicked += (s, e) => PageSelected(this, MasterPageEvent.Reset_Data);
        }
    }
}
