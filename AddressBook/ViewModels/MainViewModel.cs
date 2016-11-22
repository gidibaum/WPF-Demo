using System;
using Base;
using Base.WPF;
using System.Windows;
using Microsoft.Win32;
using AddressBook.Views;
using AddressBook.Models;


namespace AddressBook.ViewModels
{
    public class MainViewModel : ObservableObject, IDisposable
    {
        public Command ExitCommand { get; }
        public Command ConsoleCommand { get; }
        public Command CalcCommand { get; }
        public Command AboutCommand { get; }
        public Command OpenFileCommand { get; }


        public Configurator Config { get; protected set; }


        public MainViewModel()
        {
            WinConsole.Initialize();

            ExitCommand = new Command(Exit);

            ConsoleCommand = new Command(p => WinConsole.Visible = !WinConsole.Visible);

            AboutCommand = new Command(ShowAboutDialog);

            CalcCommand = new Command(Calculate);

            OpenFileCommand = new Command
            {
                CanExecuteFunction = p => true,
                ExecuteFunction = p => OpenFile()
            };


            Config = new Configurator();

            Initialize();
        }


        void Initialize()
        {
            Config.Initialize();
        }

        void Calculate()
        {
            Console.WriteLine("Calculate...");
        }


        void OpenFile()
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                Console.WriteLine("OpenFile...");
            }
        }

        void ShowAboutDialog()
        {
            var dlg = new AboutDialog()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                DataContext = this
            };

            dlg.ShowDialog();
        }

        public void Exit()
        {
            if (MessageBox.Show("Do you want to close?", "Exit", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        public void Dispose()
        {
            
        }
    }
}
