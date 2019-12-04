using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{

    [DesignTimeVisible(false)]
    public partial class FirstPage : ContentPage
    {
        // **************************** Constructor *****************************
        public FirstPage()
        {
            InitializeComponent();

            //Attach event handlers
            EditButton.Clicked += EditButton_Clicked;
        }

        // **************************** Event handlers *****************************
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var nextPage = new YearEditPage();
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
