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
            PlanetListView.SelectionMode = ListViewSelectionMode.Single;        //Default
            PlanetListView.ItemTapped += PlanetListView_ItemTapped;             //User tapped
            PlanetListView.ItemSelected += PlanetListView_ItemSelectedAsync;    //Selection changed
        }

        //Called if the item selection changes (by any means)
        private async void PlanetListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            //If nothing is selected, there is nothing to do
            if (e.SelectedItem == null) return;

            //Extract data
            string itemString = (string)e.SelectedItem;
            int selectedRow = e.SelectedItemIndex;

            //Update ViewModel
            await vm.ItemSelectionChangedAsync(row: selectedRow, planetString: itemString);
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
