using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConverter.Behavior
{
    public delegate void ExecuteCommandDelegate(object parameter);
    public delegate bool CanExecuteCommandDelegate(object parameter);
    public delegate void ExecuteCommandDelegate<T>(T parameter);
    public delegate bool CanExecuteCommandDelegate<T>(T parameter);

    public class DelegateCommand : ICommand
    {
        private ExecuteCommandDelegate execute;
        private CanExecuteCommandDelegate canExecute;

        public DelegateCommand(ExecuteCommandDelegate execute)
            : this(execute, null)
        { }

        public DelegateCommand(ExecuteCommandDelegate execute, CanExecuteCommandDelegate canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            return this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
    public class DelegateCommand<T> : ICommand
    {
        private ExecuteCommandDelegate<T> execute;
        private CanExecuteCommandDelegate<T> canExecute;

        public DelegateCommand(ExecuteCommandDelegate<T> execute)
            : this(execute, null)
        { }

        public DelegateCommand(ExecuteCommandDelegate<T> execute, CanExecuteCommandDelegate<T> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            return this.canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }
    }
}
