using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;

namespace HelloBindings
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private SayingsAbstractModel DataModel {get; }                  //Model object 
        public event PropertyChangedEventHandler PropertyChanged;       //Used to generate events to enable binding to this layer
        public IMainPageViewHelper MainPageViewHelper { get; private set; }
        public ICommand FetchNextSayingCommand { get; private set; }    //Binable command to fetch a saying

        public MainPageViewModel(SayingsAbstractModel WithModel, IMainPageViewHelper pvh)
        {
            //Reference back to ViewHelper (typically the View, but might be a unit test framework)
            MainPageViewHelper = pvh;
            
            //Set chosen data model (this may different, depending if instantiated by the view or a unit test framework
            DataModel = WithModel;

            //Hook up event handler for changes in the model
            DataModel.PropertyChanged += OnPropertyChanged;

            //Hook up button command (typically created by the view as Command is part of Xamarin.Forms)
            FetchNextSayingCommand = MainPageViewHelper.CreateConcreteCommand(  execute: async () => await DoFetchNextMessageCommand(), 
                                                                             canExecute: () => ButtonEnabled);
        }

        //Command to fetch next message - made public to support unit testing
        public async Task DoFetchNextMessageCommand()
        {
            NetworkOutcome = await DataModel.NextSaying();
            if (NetworkOutcome.success == false)
            {
                await MainPageViewHelper.ShowErrorMessageAsync(NetworkOutcome.ErrorString);
            }
        }

        //Event handler for all changes on the model
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(DataModel.SayingNumber)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.HasData)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasNoData)));
            }            
            else if (e.PropertyName.Equals(nameof(DataModel.CurrentSaying)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.IsRequestingFromNetwork)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRequestingFromNetwork)));
                MainPageViewHelper.ChangeCanExecute(FetchNextSayingCommand);
            }
        }

        //Map through read only acccess to Model properties
        public int SayingNumber => DataModel.SayingNumber;
        public string CurrentSaying => DataModel.CurrentSaying;
        public bool IsRequestingFromNetwork => DataModel.IsRequestingFromNetwork;
        public (bool success, string ErrorString) NetworkOutcome { get; set; }
        public bool HasNoData => !DataModel.HasData;
        
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
                    MainPageViewHelper.ChangeCanExecute(FetchNextSayingCommand);
                }
            }
        }

    }
}
