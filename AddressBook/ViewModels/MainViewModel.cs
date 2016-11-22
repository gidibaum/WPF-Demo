using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Base;
using Base.WPF;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.Win32;
using AddressBook.Views;
using AddressBook.Models;
using AddressBook.Services;
using System.Windows.Data;
using System.ComponentModel;

namespace AddressBook.ViewModels
{
    public class MainViewModel : ObservableObject, IDisposable
    {
        public Command ExitCommand { get; }
        public Command ConsoleCommand { get; }

        public Command AddCommand { get; }
        public Command DeleteCommand { get; }
        public Command EditCommand { get; }


        public Command CalcCommand { get; }
        public Command AboutCommand { get; }
        public Command OpenFileCommand { get; }
        public Command LoadCommand { get; }
        public Command SaveCommand { get; }

        public Command ClearFilterCommand { get; }


        #region Property: FilterText

        string _FilterText;

        public string FilterText
        {
            get { return _FilterText; }
            set
            {
                SetProperty(ref _FilterText, value);
                PeopleView.Refresh();
            }
        }

        #endregion


        public bool CanDelete => SelectedPerson != null;
        public bool CanEdit => SelectedPerson != null;



        public ObservableCollection<Person> People { get; }


        public ICollectionView PeopleView { get; }


        #region Property: SelectedPerson

        Person _SelectedPerson;

        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                SetProperty(ref _SelectedPerson, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #endregion

   
        public Configurator Config { get; protected set; }


        public MainViewModel()
        {
            WinConsole.Initialize();

            ExitCommand = new Command(Exit);

            ConsoleCommand = new Command(p => WinConsole.Visible = !WinConsole.Visible);

            AddCommand = new Command(Add);
            DeleteCommand = new Command(Delete) {CanExecuteFunction = p=>CanDelete};
            EditCommand = new Command(Edit) { CanExecuteFunction = p => CanEdit };

            AboutCommand = new Command(ShowAboutDialog);

            LoadCommand = new Command(Load);

            SaveCommand = new Command(Save);

            ClearFilterCommand = new Command(() => FilterText = "");

            CalcCommand = new Command(Calculate);

            OpenFileCommand = new Command
            {
                CanExecuteFunction = p => true,
                ExecuteFunction = p => OpenFile()
            };

            People = new ObservableCollection<Person>();

            Config = new Configurator();

            PeopleView = CollectionViewSource.GetDefaultView(People);

            PeopleView.Filter = obj =>
            {
                if (FilterText.IsEmpty()) return true;

                var p = obj as Person;
                return p != null && p.FullName.IndexOf(FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0;
            };


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

        void Add()
        {
            var p = new Person();

            var dlg = new PersonEditDialog
            {
                DataContext = p,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (dlg.ShowDialog() == true)
            {
                People.Add(p);
            }
        }

        public void Edit()
        {
            if (SelectedPerson == null) return;
            var p = SelectedPerson.Clone();
            var dlg = new PersonEditDialog
            {
                DataContext = p,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (dlg.ShowDialog() == true)
            {
                SelectedPerson.Copy(p);
            }
        }

        void Delete()
        {            
            People.Remove(SelectedPerson);
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
