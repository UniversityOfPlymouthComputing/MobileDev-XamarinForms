using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class FirstPageViewModel : INotifyPropertyChanged
    {
        //Model
        private PersonDetailsModel model;
        public PersonDetailsModel Model
        {
            get => model;
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged();
                }
                
            }
        }

        //Useful property to reference the navigation page
        protected INavigation Navigation => Application.Current.MainPage.Navigation;

        //Event handling
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ButtonCommand { get; set; }


        //Bound Data Properties Exposed to the View (read only in this case)
        public string Name => Model.Name;
        public int BirthYear => Model.BirthYear;
        public string BackButtonTitle => "Back";

        //Create events when properties change
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Main constructor
        public FirstPageViewModel(PersonDetailsModel m = null)
        {
            //Instantiate the model if one is not passed by parameter
            Model = m ?? new PersonDetailsModel("Anonymous");

            //Subscribe to changes in the model
            model.PropertyChanged += OnModelPropertyChanged;

            //The command property - bound to a button in the view
            ButtonCommand = new Command(execute: NavigateToYearEditPage);
        }

        //Watch for events on the model object
        protected void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Flag changes to the view-viewmodel binding layer -  very simple pass-through in this example
            if (e.PropertyName.Equals(nameof(Model.BirthYear)))
            {
                OnPropertyChanged(nameof(BirthYear));
            }
            else if (e.PropertyName.Equals(nameof(Model.Name)))
            {
                OnPropertyChanged(nameof(Name));
            }
        }

        // Navigate to the About page - providing both View and ViewModel pair
        void NavigateToYearEditPage()
        {
            //This has a concrete reference to a view inside a VM - is this good/bad/indifferent?

            // Create viewmodel and pass datamodel as a parameter
            // NOTE that Model is a reference type
            YearEditPageViewModel avm = new YearEditPageViewModel(Model); //VM knows about its model (reference)

            // Instantiate the view, and provide the viewmodel
            YearEditPage about = new YearEditPage(avm); //View knows about it's VM
            Navigation.PushAsync(about);
        }

        // WHAT IS NOT DONE or SHOWN
        // Consider dependency injection to perform VM-first navigation
        // This is complex but does all the wiring / instantiation for you.
        // Suggest a 3rd party MVVM framework e.g. Prism to name just one
    }
}
