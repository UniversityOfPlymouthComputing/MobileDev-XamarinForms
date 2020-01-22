using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase;
using Xamarin.Forms;
using System.Linq;


// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    public class MainPageViewModel : ViewModelBase
    {
        //**********************  PRIVATE MEMBER VARIABLES *********************

        private IMainPageHelper _viewHelper;
        private ObservableCollection<PlanetGroup> _planetGroups;
        private bool _selectionModeOn = true;
        private string _titleString = "Nothing Selected";
        private SolPlanet _selectedPlanet;
        private int _selectedRow = 0;
        private int _selectionCount = 0;
        private int _tapCount = 0;

        // ***********************  BINDABLE PROPERTIES ************************

        // ItemSource binds to an IEnumerable
        public ObservableCollection<PlanetGroup> PlanetGroups {
            get => _planetGroups;

            set
            {
                if (_planetGroups == value) return;
                _planetGroups = value;
                OnPropertyChanged();
            }
        }

        public bool SelectionModeOn {
            get => _selectionModeOn;
            set
            {
                if (_selectionModeOn == value) return;

                _selectionModeOn = value;
                OnPropertyChanged();
            }
        }

        public string TitleString {
            get => _titleString ?? "Nothing Selected";
            set
            {
                if (_titleString == value) return;
                _titleString = value;
                OnPropertyChanged();
            }
        }

        //This property is updated if the ListView selection changes by any means but ONLY if the selection changes
        public SolPlanet SelectedPlanet
        {
            get => _selectedPlanet;
            set
            {
                if (_selectedPlanet == value) return;

                _selectedPlanet = value;

                //Update UI
                TitleString = _selectedPlanet?.Name ?? "Nothing Selected";
            }
        }

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

        public int SelectionCount {
            get => _selectionCount;
            set
            {
                if (_selectionCount == value) return;
                _selectionCount = value;
                OnPropertyChanged(nameof(CounterString));
            }
        }

        public int TapCount {
            get => _tapCount;
            set
            {
                if (_tapCount == value) return;
                _tapCount = value;
                OnPropertyChanged(nameof(CounterString));
            }
        }

        public string CounterString => $"Taps: {TapCount}, Selections: {SelectionCount}";

        public ICommand DeleteCommand { get; private set; }

        

        // **************************  EVENT HANDLERS **************************

        //Event handler for user tap
        public void UserTappedList(int row, SolPlanet planet)
        {
            SelectedRow = row;
            TapCount += 1;
            string item = $"{planet.Name} - Row {row} tapped";
            PlanetGroups[2].Add(new SolPlanet(item, planet.Distance));
            _viewHelper.ScrollToObject(item);
        }

        //Event handler for selection changed
        public void ItemSelectionChanged(int row, SolPlanet planet)
        {
            SelectedRow = row;
            SelectionCount += 1;
        }

        //Menu item event - delete
        public void DeleteItem(SolPlanet p) => groupWithPlanet(p)?.Remove(p);

        // ***************************  CONSTRUCTOR ****************************
        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            //Collection of collections
            PlanetGroups = new ObservableCollection<PlanetGroup>()
            {
                new PlanetGroup("Explored", "Exp") {
                    new SolPlanet("Earth", 147.1),
                    new SolPlanet("Mars", 238.92)
                },
                new PlanetGroup("Unexplored","Uex") {
                    new SolPlanet("Mercury", 69.543),
                    new SolPlanet("Venus", 108.62),
                    new SolPlanet("Jupiter", 782.32),
                    new SolPlanet("Saturn", 1498.3),
                    new SolPlanet("Pluto", 5906.4)
                },
                new PlanetGroup("Clicked","Clk")
            };

            DeleteCommand = new Command<SolPlanet>(execute: (p) =>
            {
                DeleteItem(p);
            });
        }

        private PlanetGroup groupWithPlanet(SolPlanet p)
        {
            foreach (PlanetGroup grp in PlanetGroups)
            {
                if (grp.Contains(p)) return grp;
            }
            return null;
        }

        public MainPageViewModel() : base(null)
        {
            throw new System.Exception("You cannot call the parameterless constructor");
        }

    } //END OF CLASS
} //END OF NAMESPACE
