using System.Threading.Tasks;

namespace HelloBindings
{
    interface IPageViewHelper
    {
        Task ShowErrorMessageAsync(string ErrorMessage);
    }
}
