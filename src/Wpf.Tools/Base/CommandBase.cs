using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Tools.Helpers;

namespace Wpf.Tools.Base
{
    public abstract class AsyncCommandBase : ICommand
    {
        private bool _isDisabled;

        public virtual bool CanExecute(object parameter) => !_isDisabled;

        public async void Execute(object parameter)
        {
            Disable();

            await Task.Run(async () =>
            {
                try
                {
                    await ExecuteExternal(parameter);
                }
                catch (Exception e)
                {
                    this.LogCriticalException(e);
                }
            });

            Enable();
        }

        public event EventHandler CanExecuteChanged;

        protected abstract Task ExecuteExternal(object parameter);

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

    public class Command : AsyncCommandBase
    {
        private readonly Predicate<object> _canExecute;
        private readonly Func<object, Task> _execute;

        public Command(Func<object, Task> execute, Predicate<object> canExecute = null)
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

        protected override Task ExecuteExternal(object parameter) => _execute(parameter);
    }
}