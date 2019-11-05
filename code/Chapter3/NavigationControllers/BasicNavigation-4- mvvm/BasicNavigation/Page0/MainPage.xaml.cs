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
    public partial class MainPage : ContentPage, IMainPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this);
            //NavigationPage.SetBackButtonTitle(this, "Top");
        }

        public async Task NavigateToAboutPageAsync(string name)
        {
            AboutPage about = new AboutPage(name);
            await Navigation.PushAsync(about, true);
        }

    }
}
