using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class NameEditPageViewModel : ViewModelBase
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

        public ICommand ButtonCommand { get; set; }

        //Constructor - note that a reference to the model is NOT provided in this case
        public NameEditPageViewModel(string name)
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
            MessagingCenter.Send<NameEditPageViewModel, string>(this, "NameUpdate" ,Name);
            Navigation.PopAsync();
        }


    }
}
