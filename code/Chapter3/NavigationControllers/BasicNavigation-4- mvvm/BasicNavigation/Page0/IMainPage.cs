using System.Threading.Tasks;
using Xamarin.Forms;

namespace BasicNavigation
{
    public interface IMainPage
    {
        INavigation Navigation { get; } //All pages have this
        Task NavigateToAboutPageAsync(string name);
    }
}
