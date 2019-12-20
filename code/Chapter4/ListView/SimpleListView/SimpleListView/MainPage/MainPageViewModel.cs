using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase;
using Xamarin.Forms;


// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    public class MainPageViewModel : ViewModelBase
    {
        private IMainPageHelper _viewHelper;

        private string _titleString = "Nothing Selected";
        public string TitleString {
            get => _titleString;
            set
            {
                if (_titleString == value) return;
                _titleString = value;
                OnPropertyChanged();
            }
        }

        private int _selectedRow=0;
        public int SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (_selectedRow == value) return;
                _selectedRow = value;
                OnPropertyChanged();
            }
        }

        // ItemSource binds to an IEnumerable
        private List<string> _planets;
        public List<string> Planets {
            get => _planets;
            set => _planets = value;
        }

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


        // **************************  ITEM SELECTION **************************

        //This property is updated if the ListView selection changes by any means
        //but ONLY if the selection changes
        private string _selectedString = "";
        public string SelectedString
        {
            get => _selectedString;
            set
            {
                if (_selectedString == value) return;
                _selectedString = value;

                //Update UI
                TitleString = _selectedString;
            }
        }

        //Event handler for user tap
        public void UserTappedList(int row, string planetString)
        {
            SelectedRow = row;
            Console.WriteLine($"USER TAPPED EVENT: {planetString}, on row {row}");
        }

        //Event handler for selection changed
        public async Task ItemSelectionChangedAsync(int row, string planetString)
        {
            SelectedRow = row;
            await _viewHelper.TextPopup("Selection Changed", $"{planetString} on row {row}");
        }
    }
}
