using System;
using System.Windows.Input;

namespace HelloBindings
{
    interface ICommandFactory
    {
        ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute);
    }
}
