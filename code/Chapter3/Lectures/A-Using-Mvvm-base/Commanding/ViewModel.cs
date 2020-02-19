using System;
using System.ComponentModel;
using System.Windows.Input;
using uoplib.mvvm;
using Xamarin.Forms;

namespace Commanding
{
    public class ViewModel : ViewModelBase
    {
        private string _titleText = "Waiting Orders";
        public string TitleText
        {
            get => _titleText;
            set => Update(ref _titleText, value);
        }

        public ICommand ButtonCommand { get; set; }
        public ViewModel() : base(null)
        {
            ButtonCommand = new Command(execute: () => TitleText = "Engage");
        }
    }
}
