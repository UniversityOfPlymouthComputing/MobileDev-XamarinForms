using System.Threading.Tasks;
using System.Windows.Input;

namespace HelloBindings
{
    //Generic helper APIs for any Page
    public interface IPageViewHelper
    {
        //Display an error message in an alert box
        Task ShowErrorMessageAsync(string ErrorMessage);
        //Delegate for handling the specific ChangeCanExecute on Command 
        void ChangeCanExecute(ICommand obj);
    }
}
