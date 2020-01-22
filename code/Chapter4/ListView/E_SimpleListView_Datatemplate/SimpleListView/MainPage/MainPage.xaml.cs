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

            // *****************************************************************
            // DATA TEMPLATE
            // *****************************************************************

            //Data template will instantiate a cell "when required"

            //Two options: I like the second but the first is more common
            
            //DataTemplate DataTemplate = new DataTemplate(typeof(TextCell)); 
            DataTemplate dataTemplate = new DataTemplate( () =>
            {
                //Return a subclass of Cell
                TextCell cell = new TextCell();
                return cell;
            });

            //Binding proxy: When the DataTemplate instantiates a cell, it will also set up the binding as specified below
            //The source will be a data elelement
            dataTemplate.SetBinding(TextCell.TextProperty, "Name");
            dataTemplate.SetBinding(TextCell.DetailProperty, "Distance");

            //Finally, set the ItemTemplate property (type DataTemplate)
            PlanetListView.ItemTemplate = dataTemplate;
            // *****************************************************************
        }

        //Called if the item selection changes (by any means)
        private void PlanetListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //If nothing is selected, there is nothing to do
            if (e.SelectedItem == null) return;

            //Extract data
            SolPlanet item = (SolPlanet)e.SelectedItem;
            int selectedRow = e.SelectedItemIndex;

            //Update ViewModel
            vm.ItemSelectionChanged(row: selectedRow, planet: item);
        }

        //Called if user taps a row (as opposed to programatically changing the selection)
        private void PlanetListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SolPlanet item = (SolPlanet)e.Item;
            int selectedRow = e.ItemIndex;
            vm.UserTappedList(row: selectedRow, planet: item);
        }

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
