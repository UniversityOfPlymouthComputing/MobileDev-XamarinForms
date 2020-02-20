using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace uoplib.mvvm
{

    public abstract class ViewModelBase : MVVMBase, INotifyPropertyChanged
    {
        //Useful property to reference the navigation page
        public INavigation Navigation { get; private set; } = null;

        public ViewModelBase(INavigation navigation = null) => Navigation = navigation;
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

        public ViewModelBase(INavigation nav = null) : base(nav) { }

    }
}
