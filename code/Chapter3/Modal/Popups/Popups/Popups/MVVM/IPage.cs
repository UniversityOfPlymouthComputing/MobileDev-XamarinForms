using Xamarin.Forms;

namespace Popups
{
    public interface IPage
    {
        INavigation NavigationProxy { get; }
    }
}
