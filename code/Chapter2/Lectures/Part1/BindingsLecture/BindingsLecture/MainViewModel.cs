using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BindingsLecture
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _greeting = "Label 3";
        public string Greeting {
            get => _greeting;
            set
            {
                if (value == _greeting) return;
                _greeting = value;
                OnPropertyChanged();
            }
        }

        public ICommand Button4Command { get; set; }

        private void Button4CommandHandler()
        {
            Greeting = "Button 4 Tapped";
        }

        public MainViewModel()
        {
            Button4Command = new Command(execute: Button4CommandHandler);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
