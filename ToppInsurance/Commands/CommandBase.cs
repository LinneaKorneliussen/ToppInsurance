using System.Windows.Input;

namespace TopInsuranceWPF.Commands
{
    /// <summary>
    /// The CommandBase class is an abstract implementation of the ICommand interface, designed to facilitate the command pattern in WPF applications. 
    /// It provides a foundation for creating commands by defining the CanExecute and Execute methods, which must be implemented by derived classes. 
    /// The class also includes an event, CanExecuteChanged, which allows for automatic updates to the command's executable state through the CommandManager. 
    /// This structure enables the binding of user interface actions to command logic, promoting a clean separation of concerns in the application architecture.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
