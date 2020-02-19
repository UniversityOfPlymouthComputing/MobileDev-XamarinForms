using System;
using System.ComponentModel;
using System.Windows.Input;
using uoplib.mvvm;
using Xamarin.Forms;

namespace Commanding
{
    public class ViewModel : ViewModelBase
    {
        private StarFleetStaff Model = new StarFleetStaff("Engineering", "Gold");

        private string _titleText = "Star Fleet Status";
        public string TitleText
        {
            get => _titleText;
            set => Update(ref _titleText, value);
        }

        public ICommand ButtonCommand { get; set; }
        public ViewModel(INavigation nav) : base(nav)
        {
            
            ButtonCommand = new Command(execute: () => {
                var vm = new RankSelectionViewModel(nav, Model);
                var detailPage = new RankSelectionPage(vm);
                Navigation.PushAsync(detailPage);

            });
        }
    }
}
