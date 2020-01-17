using System.ComponentModel;
using System.Threading.Tasks;
using MVVMBase;
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

            // Hook up event handlers
            PlanetListView.SelectionMode = ListViewSelectionMode.None;        //Default
            PlanetListView.ItemTapped += PlanetListView_ItemTapped;           //User tapped
        }

        //Called if user taps a row (as opposed to programatically changing the selection)
        private async void PlanetListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string itemString = (string)e.Item;
            int selectedRow = e.ItemIndex;
            await vm.UserTappedListAsync(row: selectedRow, planetString: itemString);
        }

        INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }
    }
}
