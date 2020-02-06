using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using uoplib.mvvm;
using Xamarin.Forms;

namespace SimpleListView
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, IMainPageHelper
    {
        private MainPageViewModel vm;

        public MainPage()
        {
            InitializeComponent();

            //Set binding context
            vm = new MainPageViewModel(this);
            BindingContext = vm;

        }

        // **************** IMainPageHelper **************** 
        INavigation IPage.NavigationProxy => Navigation;
        public async Task TextPopup(string title, string message) => await DisplayAlert(title, message, "Dismiss");
        public void ScrollToObject(object obj) => PlanetListView.ScrollTo(obj, ScrollToPosition.End, true);
    }
}
