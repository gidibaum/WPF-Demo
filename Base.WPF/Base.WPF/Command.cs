using System;
using System.Windows.Input;
using System.Windows;

//-----------------------------------------------------------------------      

namespace Base.WPF
{
    public class Command : ICommand
    {
        public Command()
        {

        }

        public Command(Action<object> action)
        {
            CanExecuteFunction = p => true;
            ExecuteFunction = p => action(p);
        }

        public Command(Action action)
        {
            CanExecuteFunction = p => true;
            ExecuteFunction = p => action();
        }

        public delegate void ExecuteDelegate(object parameter);
        public delegate bool CanExecuteDelegate(object parameter);

        public ExecuteDelegate ExecuteFunction {get; set;}
        public CanExecuteDelegate CanExecuteFunction {get; set;}


        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunction != null)
                return CanExecuteFunction(parameter);

            return false;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public void Execute(object parameter)
        {
            if (ExecuteFunction != null)
                ExecuteFunction(parameter);
        }

        #endregion


    }

    //-----------------------------------------------------------------------      
    //-----------------------------------------------------------------------      

    /// This class facilitates associating a key binding in XAML markup to  a command
    /// defined in a View Model by exposing a Command dependency property.
    /// The class derives from Freezable to work around a limitation in WPF when data-binding from XAML.
    /// </summary>
    public class CommandReference : Freezable, ICommand
    {

        public CommandReference()
        {
            // Blank
        }


        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandReference), 
            new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        #region ICommand Members


        public bool CanExecute(object parameter)
        {
            if (Command != null)
                return Command.CanExecute(parameter);

            return false;
        }


        public void Execute(object parameter)
        {
            Command.Execute(parameter);
        }


        public event EventHandler CanExecuteChanged;


        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandReference commandReference = d as CommandReference;
            ICommand oldCommand = e.OldValue as ICommand;
            ICommand newCommand = e.NewValue as ICommand;

            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= commandReference.CanExecuteChanged;
            }
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += commandReference.CanExecuteChanged;
            }
        }


        #endregion

        #region Freezable


        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
