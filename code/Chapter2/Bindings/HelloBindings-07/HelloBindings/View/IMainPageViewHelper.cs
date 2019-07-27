using System.Threading.Tasks;

namespace HelloBindings
{
    interface IMainPageViewHelper : ICommandFactory, IPageViewHelper
    {
        Task ShowModalAboutPageAsync();
    }
}
