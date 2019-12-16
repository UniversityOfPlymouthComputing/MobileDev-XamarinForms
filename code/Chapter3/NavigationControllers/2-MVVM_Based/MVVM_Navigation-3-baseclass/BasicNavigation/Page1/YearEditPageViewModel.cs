using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class YearEditPageViewModel : ViewModelBase<PersonDetailsModel>
    {
        //Event handling
        public ICommand ButtonCommand { get; set; }
        public ICommand BirthYearSliderCommand { get; set; }

        //Bound Data Properties Exposed to the View
        public string Name => Model.Name;   //Read Only

        public int BirthYear                //Read / Write
        {
            get => Model.BirthYear;
            set
            {
                if (value != Model.BirthYear)
                {
                    Model.BirthYear = value;
                    // No need to call OnPropertyChanged() as it's handled in `OnModelPropertyChanged`
                }
            }
        }

        //Main constructor
        public YearEditPageViewModel(PersonDetailsModel model = null)
        {
            //Instantiate the model
            Model = model ?? new PersonDetailsModel("Anon");

            //Subscribe to changes in the model - now hooked up in the baseclass
            //model.PropertyChanged += OnModelPropertyChanged;

            //Command property - bound to a button in the view
            ButtonCommand = new Command(execute: NavigateToNameEditPage);

            //Command property - save the model only when the user stops moving the slider
            BirthYearSliderCommand = new Command(execute: () => Model.Save());
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
        void NavigateToNameEditPage()
        {
            MessagingCenter.Subscribe<NameEditPageViewModel, string>(this, "NameUpdate", (sender, arg) =>
            {
                Model.Name = arg;
                Model.Save();
            });

            //This has a concrete reference to a view inside a VM - is this good/bad/indifferent?

            // Create viewmodel and pass Model.Name (string) as a parameter
            // NOTE that string is an immutable reference type
            NameEditPageViewModel vm = new NameEditPageViewModel(Model.Name); 

            // Instantiate the view, and provide the viewmodel
            NameEditPage nextPage = new NameEditPage(vm); //View knows about it's VM
            Navigation.PushAsync(nextPage);
        }
    }
}
