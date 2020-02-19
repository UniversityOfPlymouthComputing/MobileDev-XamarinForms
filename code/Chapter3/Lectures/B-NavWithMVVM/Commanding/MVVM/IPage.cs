using Xamarin.Forms;

namespace uoplib.mvvm
{
    public interface IPage
    {
        INavigation NavigationProxy { get; }
    }
}
