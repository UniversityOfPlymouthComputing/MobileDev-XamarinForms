using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace BasicNavigation
{
    public class MainPageViewModel : ViewModelBase<PersonDetailsModel>
    {

        //Event handling
        public ICommand ButtonCommand { get; set; }

        //Bound Data Properties Exposed to the View (read only in this case)
        public string Name => Model.Name;
        public int BirthYear => Model.BirthYear;

        //Main constructor
        public MainPageViewModel()
        {
            //Instantiate the model
            string mainDir = FileSystem.AppDataDirectory;
            string path = mainDir + "userdetails.txt";

            Model = BindableModelBase.Load<PersonDetailsModel>(path);
            if (Model == null)
            {
                Model = new PersonDetailsModel("NickO");
                Model.Save(path);
            }

            //Subscribe to changes in the model
            //Model.PropertyChanged += OnModelPropertyChanged;

            //The command property - bound to a button in the view
            ButtonCommand = new Command(execute: NavigateToAboutPage);
        }

        //Watch for events on the model object
        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
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
        void NavigateToAboutPage()
        {
            //This has a concrete reference to a view inside a VM - is this good/bad/indifferent?

            // Create viewmodel and pass datamodel as a parameter
            // NOTE that Model is a reference type
            AboutPageViewModel avm = new AboutPageViewModel(Model); //VM knows about its model (reference)

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
