using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ProfoundSayings;
using System.Threading.Tasks;
using System;

namespace HelloBindings
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private ISayingsModel DataModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ButtonCommand { get; private set; }

        public MainPageViewModel() : this(new LocalModel())
        {
            //Default use the Model class
            // Constructor chaining
        }

        public MainPageViewModel(ISayingsModel WithModel)
        {
            DataModel = WithModel;
            ButtonCommand = new Command(execute: async () => await ShowNextMessageCommand(), canExecute: () => UIVisible);
            DataModel.PropertyChanged += OnPropertyChanged;

        }

        //Listen for changes on the model
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(DataModel.SayingNumber)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.CurrentSaying)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
            }
        }

        //Command to show next message
        async Task ShowNextMessageCommand()
        {
            await DataModel.NextSayingAsync();
        }

        public int SayingNumber => DataModel.SayingNumber;
        public string CurrentSaying => DataModel.CurrentSaying;

        private bool _visible = true;
        public bool UIVisible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UIVisible)));
                    ((Command)ButtonCommand).ChangeCanExecute();
                }
            }
        }

    }
}
