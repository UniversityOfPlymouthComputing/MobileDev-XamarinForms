using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HelloBindings
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private Model DataModel = new Model();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ButtonCommand { get; private set; }

        public MainPageViewModel()
        {
            ButtonCommand = new Command(execute: () =>
            {
                DataModel.NextMessage();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSaying"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SayingNumber"));
            }, canExecute: () => this.UIVisible);
        }

        public int SayingNumber => DataModel.SayingNumber;

        public string CurrentSaying => DataModel.CurrentSaying;

        bool _visible = true;
        public bool UIVisible
        {
            get => _visible;
            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UIVisible"));
                    ((Command)ButtonCommand).ChangeCanExecute();
                }
            }
        }

    }
}
