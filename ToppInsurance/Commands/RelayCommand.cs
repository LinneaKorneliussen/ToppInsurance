
namespace TopInsuranceWPF.Commands
{
    /// <summary>
    /// The RelayCommand class implements the CommandBase abstract class and serves as a command handler in WPF applications, 
    /// facilitating the execution of actions in response to user interface events. It encapsulates the logic for command execution and determining whether a command can be executed. 
    /// The class provides constructors that accept an Action delegate for the command's execution logic and an optional Func<bool> delegate to evaluate if the command can be executed.
    /// Additionally, the RelayCommand<T> class extends RelayCommand to support commands that require a parameter of a specific type.
    /// It allows the execution of an Action<T> and the evaluation of a corresponding Func<T, bool> to check the command's executability based on the provided parameter. 
    /// This structure enhances the flexibility and reusability of command logic within the MVVM (Model-View-ViewModel) pattern.
    /// </summary>
    public class RelayCommand : CommandBase
    {
        private readonly Action? _execute = null;
        private readonly Func<bool>? _canExecute = null;

        public RelayCommand() { }
        public RelayCommand(Action execute) : this(execute, null!) { }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override void Execute(object parameter) { _execute(); }
        public override bool CanExecute(object parameter) => _canExecute == null || _canExecute();

    }

    public class RelayCommand<T> : RelayCommand
    {
        private readonly Action<T> _execute = null;
        private readonly Func<T, bool> _canExecute = null;

        public RelayCommand(Action<T> execute) : this(execute, null!) { }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override void Execute(object parameter) { _execute((T)parameter); }
        public override bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);
    }
}
