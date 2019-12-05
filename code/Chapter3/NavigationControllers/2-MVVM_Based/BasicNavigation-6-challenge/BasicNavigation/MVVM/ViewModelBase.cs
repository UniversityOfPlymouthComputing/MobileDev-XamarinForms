using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BasicNavigation
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Useful property to reference the navigation page
        protected INavigation Navigation => Application.Current.MainPage.Navigation;

        //Create events when properties change
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class ViewModelBase<DataModelType> : ViewModelBase where DataModelType : BindableModelBase
    {
        //Model
        private DataModelType model;
        public DataModelType Model
        {
            get => model;
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged();
                    if (model != null)
                    {
                        model.PropertyChanged += OnModelPropertyChanged;
                    }
                }
            }
        }

        protected abstract void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e);
        /*
                // EXAMPLE
                protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    //Flag changes to the view-viewmodel binding layer -  very simple pass-through in this example
                    if (e.PropertyName.Equals(nameof(Model.BirthYear)))
                    {
                        OnPropertyChanged(nameof(BirthYear));
                    }
                    else if (e.PropertyName.Equals(nameof(Model.Name)))
                    {
                        OnPropertyChanged(nameof(Name));
                    }
                }
         */

    }
}
