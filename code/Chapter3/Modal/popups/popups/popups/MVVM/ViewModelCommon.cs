using System;
using MVVMBase;
using Xamarin.Forms;

namespace PhoneFeatureApp
{
    public class ViewModelCommon : ViewModelBase
    {
        //Background color
        private Color backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                if (backgroundColor != value)
                {
                    backgroundColor = value;
                    OnPropertyChanged();
                }

            }
        }
        protected void subscribeToBackgroundColChange()
        {
            MessagingCenter.Subscribe<ViewModelCommon, Color>(this, "BackgroundColorChange", (sender, arg) => BackgroundColor = arg);
        }

        public ViewModelCommon(INavigation nav) : base(nav) { }
    }
}
