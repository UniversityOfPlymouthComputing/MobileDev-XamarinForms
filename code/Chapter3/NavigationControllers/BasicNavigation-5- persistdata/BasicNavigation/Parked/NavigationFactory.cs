using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class NavigationFactory
    {
        private Dictionary<ViewModelBase, Page> vvm = new Dictionary<ViewModelBase, Page>();

        private static NavigationFactory instance;
        public static NavigationFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NavigationFactory();
                }
                return instance;
            }
        }

        private NavigationFactory()
        {
        }

        public void NavigateTo(ViewModelBase vm)
        {
            Page page = vvm[vm];
            page.BindingContext = vm;
            page.Navigation.PushAsync(page);
        }

        public void Register<PageType>(ViewModelBase vm) where PageType : Page
        {
            vvm[vm] = (Page)Activator.CreateInstance(typeof(PageType));
        }

        public static ICommand CreateCommand(Action action, Func<bool> canExecute)
        {
            return new Command(action, canExecute);
        }
    }
}
