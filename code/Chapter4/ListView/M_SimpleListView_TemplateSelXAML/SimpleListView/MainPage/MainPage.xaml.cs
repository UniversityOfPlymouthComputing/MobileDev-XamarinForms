using System.ComponentModel;
using System.Diagnostics;
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
            PlanetListView.ItemSelected += PlanetListView_ItemSelected;    //Selection changed

            /* The template selector could have been set up as follows
            PlanetListView.ItemTemplate = new PlanetTemplateSelector()
            {
                PageRef = this
            };
            */
        }

        //Called if the item selection changes (by any means)
        private void PlanetListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Extract data
            if (e.SelectedItem is SolPlanet item)
            {
                //Update ViewModel
                int selectedRow = e.SelectedItemIndex;
                vm.ItemSelectionChanged(row: selectedRow, planet: item);
            }
        }

        //Called if user taps a row (as opposed to programatically changing the selection)
        private void PlanetListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SolPlanet item = (SolPlanet)e.Item;
            int selectedRow = e.ItemIndex;
            vm.UserTappedList(row: selectedRow, planet: item);
        }

        // **************** IMainPageHelper **************** 

        INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }

        public void ScrollToObject(object obj)
        {
            PlanetListView.ScrollTo(obj, ScrollToPosition.End, true);
        }
    }
}
