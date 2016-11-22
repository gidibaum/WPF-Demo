using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Base;
using Base.WPF;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Win32;
using AddressBook.Views;
using AddressBook.Models;
using AddressBook.Services;


namespace AddressBook.ViewModels
{
    public class MainViewModel : ObservableObject, IDisposable
    {
        public Command ExitCommand { get; }
        public Command ConsoleCommand { get; }
        public Command CalcCommand { get; }
        public Command AboutCommand { get; }
        public Command OpenFileCommand { get; }
        public Command LoadCommand { get; }
        public Command SaveCommand { get; }


        public ObservableCollection<Person> People { get; }


        #region Property: SelectedPerson

        Person _SelectedPerson;

        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set { SetProperty(ref _SelectedPerson, value); }
        }

        #endregion


        public Configurator Config { get; protected set; }


        public MainViewModel()
        {
            WinConsole.Initialize();

            ExitCommand = new Command(Exit);

            ConsoleCommand = new Command(p => WinConsole.Visible = !WinConsole.Visible);

            AboutCommand = new Command(ShowAboutDialog);

            LoadCommand = new Command(Load);

            SaveCommand = new Command(Save);

            CalcCommand = new Command(Calculate);

            OpenFileCommand = new Command
            {
                CanExecuteFunction = p => true,
                ExecuteFunction = p => OpenFile()
            };

            People = new ObservableCollection<Person>();

            Config = new Configurator();

            Initialize();
        }


        void Initialize()
        {
            Config.Initialize();

            People.AddRange(PeopleRepository.CreateTestData());
        }

        async void Load()
        {
            People.Clear();

            var people = await PeopleRepository.Load();

            People.AddRange(people);
        }

        async void Save()
        {
            await PeopleRepository.Save(People);
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
