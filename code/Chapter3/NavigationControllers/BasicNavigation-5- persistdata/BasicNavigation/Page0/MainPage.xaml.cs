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
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm = null)
        {
            InitializeComponent();
            BindingContext = vm ?? new MainPageViewModel();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }
    }
}
