using System;
using System.Windows.Input;

namespace HelloBindings
{
    public interface ICommandFactory
    {
        ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute);
    }
}
