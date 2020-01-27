using System.Threading.Tasks;
using MVVMBase;

namespace SimpleListView
{
    public interface IMainPageHelper : IPage
    {
        Task TextPopup(string title, string message);
        void ScrollToObject(object obj);
    }

    
}
