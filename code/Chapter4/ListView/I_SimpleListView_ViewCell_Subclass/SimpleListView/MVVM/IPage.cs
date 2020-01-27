using Xamarin.Forms;

namespace MVVMBase
{
    public interface IPage
    {
        INavigation NavigationProxy { get; }
    }
}
