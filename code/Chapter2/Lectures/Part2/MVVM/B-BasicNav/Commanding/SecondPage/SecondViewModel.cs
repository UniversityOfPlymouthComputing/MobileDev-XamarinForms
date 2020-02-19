using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Commanding
{
    public class SecondViewModel
    {
        private INavigation Navigation;

        public ICommand DismissCommand { get; private set; }
        void DismissButtonTapped()
        {
            Console.WriteLine("By'ee");
            Navigation.PopAsync();
        }
        public SecondViewModel(INavigation nav)
        {
            Navigation = nav;
            DismissCommand = new Command(execute: DismissButtonTapped);
        }

        
    }
}
