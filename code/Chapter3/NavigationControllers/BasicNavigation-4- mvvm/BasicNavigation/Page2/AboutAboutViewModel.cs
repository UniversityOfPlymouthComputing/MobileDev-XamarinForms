using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class AboutAboutViewModel : INotifyPropertyChanged
    {
        //There is no separate model class as this ViewModel only edits a single string
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                    ((Command)ButtonCommand)?.ChangeCanExecute();
                }
            }
        }

        //Useful property to reference the navigation page
        protected INavigation Navigation => Application.Current.MainPage.Navigation;

        //Event handling
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ButtonCommand { get; set; }

        //Create events when properties change
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Constructor - note that a reference to the model is NOT provided in this case
        public AboutAboutViewModel(string name)
        {
            //Note that string is immutable and although a reference type, will be replaced if the user edits
            //without impacting on the original. Use a string builder for mutable strings
            Name = name;

            //The command property - bound to a button in the view
            ButtonCommand = new Command(execute: SaveAndNavigateBack, canExecute: () => {
                return (Name.Length > 0);       
            });
        }

        protected void SaveAndNavigateBack()
        {
            //TODO: Send result back - but how? :)
            MessagingCenter.Send<AboutAboutViewModel, string>(this, "NameUpdate" ,Name);
            Navigation.PopAsync();
        }


    }
}
