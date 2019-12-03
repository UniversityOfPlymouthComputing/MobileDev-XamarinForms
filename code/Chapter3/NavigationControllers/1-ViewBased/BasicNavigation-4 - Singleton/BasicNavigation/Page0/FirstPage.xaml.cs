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
        public FirstPage(string name = "Anon")
        {
            InitializeComponent();
            EditButton.Clicked += EditButton_Clicked;
            SingletonModel.SharedInstance.Name = name;            
        }

        // **************************** Event handlers *****************************
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            //No need to pass data forward
            var nextPage = new YearEditPage();

            //Navigate
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
