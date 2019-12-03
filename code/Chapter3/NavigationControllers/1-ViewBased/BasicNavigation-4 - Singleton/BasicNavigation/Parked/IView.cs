using Xamarin.Forms;

namespace BasicNavigation
{
    public interface IView : INavigation
    {
        NavigableElement Navigation { get; }
        BindableObject BindingContext { get; set; }
    }
}
