using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ProfoundSayings;
using System.Threading.Tasks;

namespace HelloBindings
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private SayingsAbstractModel DataModel { get; }                 //Model object 
        public event PropertyChangedEventHandler PropertyChanged;       //Used to generate events to enable binding to this layer
        public ICommand FetchNextSayingCommand { get; private set; }    //Binable command to fetch a saying

        public MainPageViewModel(SayingsAbstractModel WithModel)
        {
            DataModel = WithModel;
            //Hook up FetchNextSayingCommand property
            FetchNextSayingCommand = new Command(execute: async () => await DoFetchNextMessageCommand(), 
                                              canExecute: () => ButtonEnabled);
            //Hook up event handler for changes in the model
            DataModel.PropertyChanged += OnPropertyChanged;
        }

        //Command to fetch next message - made public to support unit testing
        public async Task DoFetchNextMessageCommand() => await DataModel.NextSaying();

        //Exent handler for all changes on the model
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(DataModel.SayingNumber)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.HasData)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.CurrentSaying)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.IsRequestingFromNetwork)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRequestingFromNetwork)));
                ((Command)FetchNextSayingCommand).ChangeCanExecute();
            }
        }

        //Map through read only acccess to Model properties
        public int SayingNumber => DataModel.SayingNumber;
        public string CurrentSaying => DataModel.CurrentSaying;
        public bool IsRequestingFromNetwork => DataModel.IsRequestingFromNetwork;
        public bool HasData => DataModel.HasData;
        
        //Calculated property for the button canExecute
        public bool ButtonEnabled => UIVisible && !IsRequestingFromNetwork;

        //Bindable property to manage UI state visibility (not to be confused with model based state)
        private bool _uiVisible = true;
        public bool UIVisible
        {
            get => _uiVisible;
            set
            {
                if (value != _uiVisible)
                {
                    _uiVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UIVisible)));
                    ((Command)FetchNextSayingCommand).ChangeCanExecute();
                }
            }
        }

    }
}
