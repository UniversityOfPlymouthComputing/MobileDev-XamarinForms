using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Commanding
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _titleText = "Waiting Orders";
        public string TitleText
        {
            get => _titleText;
            set
            {
                if (value == _titleText) return;
                _titleText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleText)));
            }
        }

        private void DoButtonCommand()
        {
            TitleText = "Engage";
        }

        public ICommand ButtonCommand { get; set; }
        public ViewModel()
        {
            ButtonCommand = new Command(execute: DoButtonCommand);
        }
    }
}
