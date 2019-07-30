using System.Threading.Tasks;

namespace HelloBindings
{
    //Helper APIs specific to MainPageView
    public interface IMainPageViewHelper : ICommandFactory, IPageViewHelper
    {
        //Display an about page
        Task ShowModalAboutPageAsync();
    }
}
