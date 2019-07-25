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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
            }, canExecute: () => this.UIVisible);

        }

        public int SayingNumber => DataModel.SayingNumber;

        public string CurrentSaying => DataModel.CurrentSaying;

        bool _visible = true;
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
