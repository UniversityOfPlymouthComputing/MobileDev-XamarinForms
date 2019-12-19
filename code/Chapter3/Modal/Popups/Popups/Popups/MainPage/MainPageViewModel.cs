using MVVMBase;
using System.Windows.Input;
using Xamarin.Forms;

namespace Popups
{
    class MainPageViewModel : ViewModelBase
    {
        private IMainPageHelper viewHelper;
        private string _name = "Anon";

        public ICommand ButtonCommand { get; set; }

        public string Name {
            get => _name; 
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainPageViewModel(IMainPageHelper p) : base(p.NavigationProxy)
        {
            viewHelper = p;
            ButtonCommand = new Command(execute: async () =>
            {
                string name = await viewHelper.AskForString("Enter Name", "What is your name?");
                if (name == null) return;

                bool save = await viewHelper.YesNoAlert("Confirm", $"Are you sure you want to set the name to {name}?");
                if (save)
                {
                    Name = name;
                }

            });
        }

        
    }
}
