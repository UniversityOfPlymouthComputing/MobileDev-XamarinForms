using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using MVVMBase;

// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    public class MainPageViewModel : ViewModelBase
    {
        //**********************  PRIVATE MEMBER VARIABLES *********************
        private List<string> _planets;
        private string _titleString = "Nothing Selected";
        private Timer tmr;
        private int _tickCount = 0;

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

        // ***************************  CONSTRUCTOR ****************************
        public MainPageViewModel(IPage viewHelper) : base(viewHelper.NavigationProxy)
        {
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

            //5 second timer
            tmr = new Timer(5000);
            tmr.Elapsed += Tmr_Elapsed;
            tmr.AutoReset = true;
            tmr.Enabled = true;
        }

        //Adds data to the List collection every 5 seconds
        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            _tickCount++;

            //VER1 - update content of the List
            /*
            Planets.Add($"Timer Fired {_tickCount} times");
            Planets[0] = "First Item Updated";
            */

            //VER2 - replace the entire list (expensive)
            
            //Make a copy
            List<string> newList = new List<string>(Planets);

            //Add new row
            newList.Add($"Timer Fired {_tickCount} times");

            //Update the complete List with a new one
            Planets = newList;
            

            TitleString = $"Timer fired {_tickCount} times";
        }
    } //END OF CLASS
} //END OF NAMESPACE
