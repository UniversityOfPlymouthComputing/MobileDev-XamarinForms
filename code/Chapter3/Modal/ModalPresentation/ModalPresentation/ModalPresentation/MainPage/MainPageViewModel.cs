using MVVMBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ModalPresentation
{
    public class MainPageViewModel : ViewModelBase
    {
        // Model data
        private double _price = 10.0;

        // Properties exposes to the View ViewModel binding layer
        public ICommand ButtonCommand { get; set; }
        public double Price {
            get => _price;
            set {
                if (_price == value) return;
                _price = value;
                OnPropertyChanged(nameof(PriceString));
            }
        }
        public string PriceString => String.Format("£ {0:0.00}", Price);

        // Navigation
        private void PresentModalPage()
        {
            //Hook up next page and associated view model
            var modalPage = new PriceEditPage();
            modalPage.BindingContext = new PriceEditPageViewModel(modalPage.Navigation, Price);

            //Present modally
            _ = Navigation.PushModalAsync(modalPage, true);
        }

        // Constructor
        public MainPageViewModel(INavigation nav) : base(nav)
        {
            ButtonCommand = new Command(execute: PresentModalPage);

            //Listen for message back from presented page
            MessagingCenter.Subscribe<PriceEditPageViewModel, double>(this, "PriceUpdate", (sender, arg) => Price = arg);
        }
    }
}
