using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BasicNavigation
{

    public abstract class ViewModelBase<ModelType> : INotifyPropertyChanged where ModelType : ModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ModelType model;
        public ModelType Model {
            get => model;
            set
            {
                if (model != value)
                {
                    model = value;
                    model.PropertyChanged += OnModelPropertyChanged;
                    OnPropertyChanged();
                }
            }
        }

        protected abstract void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e);

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase(ModelType m = null)
        {
            if (m != null)
            {
                Model = m;
            }
        }

        protected INavigation Navigation
        {
            get => Application.Current.MainPage.Navigation;
        }

    }
}
