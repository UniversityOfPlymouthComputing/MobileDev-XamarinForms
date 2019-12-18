using MVVMBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ModalPresentation
{
    public interface IPriceEditPageViewModel
    {
        void PriceEdited(double p); 
    }

    class PriceEditPageViewModel : ViewModelBase
    {
        //Model data
        private double _price = 0.0;

        // Properties exposes to the View ViewModel binding layer
        public ICommand ButtonClose { get; set; }
        public double Price
        {
            get => _price;
            set
            {
                if (_price == value) return;
                _price = value;
                OnPropertyChanged();
            }
        }


        public PriceEditPageViewModel(INavigation nav, double price = 0.0) : base(nav)
        {
            //Data passed forwards
            Price = price;

            ButtonClose = new Command(execute: () =>
            {
                // Again use MessageCentre to send data back - this time let the compiler infer the types

                //MessagingCenter.Send<PriceEditPageViewModel, double>(this, "PriceUpdate", Price);
                MessagingCenter.Send(this, "PriceUpdate", Price);
                
                //Dismiss this page
                _ = Navigation?.PopModalAsync(true);
            });
        }


    }
}
