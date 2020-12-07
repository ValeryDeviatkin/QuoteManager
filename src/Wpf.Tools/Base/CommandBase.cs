using System;
using System.Windows.Input;
using Wpf.Tools.Helpers;

namespace Wpf.Tools.Base
{
    public abstract class CommandBase : ICommand
    {
        private bool _isDisabled;

        public virtual bool CanExecute(object parameter) => !_isDisabled;

        public void Execute(object parameter)
        {
            Disable();

            try
            {
                ExecuteExternal(parameter);
            }
            catch (Exception e)
            {
                this.LogCriticalException(e);
            }

            Enable();
        }

        public event EventHandler CanExecuteChanged;

        protected abstract void ExecuteExternal(object parameter);

        private void ChangeCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private void Disable()
        {
            _isDisabled = true;

            ChangeCanExecute();
        }

        private void Enable()
        {
            _isDisabled = false;

            ChangeCanExecute();
        }
    }

    public class Command : CommandBase
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public Command(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            var isCanExecute = false;

            if (base.CanExecute(parameter))
            {
                isCanExecute = _canExecute?.Invoke(parameter) ?? true;
            }

            return isCanExecute;
        }

        protected override void ExecuteExternal(object parameter)
        {
            _execute(parameter);
        }
    }
}