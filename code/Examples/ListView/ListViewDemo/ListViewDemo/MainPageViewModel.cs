using System;
namespace ListViewDemo
{
    public class MainPageViewModel
    {
        public IMainPageView view { get; private set; }
        public MainPageViewModel(IMainPageView v)
        {
            view = v;
        }
    }
}
