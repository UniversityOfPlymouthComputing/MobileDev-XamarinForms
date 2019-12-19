using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Popups
{

    public interface IMainPageHelper : IPage
    {
        Task<bool> YesNoAlert(string title, string message);
        Task<string> AskForString(string questionTitle, string question);
    }
}
