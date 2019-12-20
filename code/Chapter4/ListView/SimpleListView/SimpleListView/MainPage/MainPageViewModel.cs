using System.Collections.Generic;
using System.Threading.Tasks;
using MVVMBase;

// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    public class MainPageViewModel : ViewModelBase
    {
        //**********************  PRIVATE MEMBER VARIABLES *********************

        private IMainPageHelper _viewHelper;
        private List<string> _planets;
        private string _titleString = "Nothing Selected";

        // ***********************  BINDABLE PROPERTIES ************************

        // ItemSource binds to an IEnumerable
        public List<string> Planets
        {
            get => _planets;
            set
            {
                if (_planets == value) return;
                _planets = value;
                OnPropertyChanged();
            }
        }

        public string TitleString {
            get => _titleString;
            set
            {
                if (_titleString == value) return;
                _titleString = value;
                OnPropertyChanged();
            }
        }

        // **************************  EVENT HANDLERS **************************

        //Event handler for user tap
        public async Task UserTappedListAsync(int row, string planetString)
        {
            TitleString = planetString;
            await _viewHelper.TextPopup("Tapped", $"{planetString} on row {row}");
        }

        // ***************************  CONSTRUCTOR ****************************
        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            Planets = new List<string>
            {
                "Mercury",
                "Venus",
                "Jupiter",
                "Earth",
                "Mars",
                "Saturn",
                "Pluto"
            };
        }

    } //END OF CLASS
} //END OF NAMESPACE
