using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
            vm = new MainPageViewModel(this);
            BindingContext = vm;

            // The ItemSelected event is now being used
            // SelectedItem bindable property is also employed
            PlanetListView.SelectionMode = ListViewSelectionMode.Single;   //Default
            //PlanetListView.SelectionMode = ListViewSelectionMode.None;   //Tap only
            PlanetListView.ItemTapped += PlanetListView_ItemTapped;        //User tapped
            PlanetListView.ItemSelected += PlanetListView_ItemSelectedAsync;    //Selection changed
        }

        //Called if the item updated (by any means)
        private async void PlanetListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            //If nothing is selected, there is nothing to do
            if (e.SelectedItem == null) return;

            //Extract data
            string itemString = (string)e.SelectedItem;
            int selectedRow = e.SelectedItemIndex;

            //Deselect the row (animate away the selection)
            //((ListView)sender).SelectedItem = null;

            //Update ViewModel
            await vm.ItemSelectionChangedAsync(row: selectedRow, planetString: itemString);
            
        }

        //Called if user taps a row (as opposed to programatically changing the selection)
        private void PlanetListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string itemString = (string)e.Item;
            int selectedRow = e.ItemIndex;
            vm.UserTappedList(row: selectedRow, planetString: itemString);
        }

        INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }
    }
}
