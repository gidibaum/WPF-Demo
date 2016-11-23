using System.ComponentModel;
using System.Windows;
using Base;
using Base.WPF;
using FileTreeDemo.Models;
using FileTreeDemo.Services;

namespace FileTreeDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public Command ExitCommand { get; }
        public Command SelectItemCommand { get; }

        public DirItem Root { get; }

        #region Property: SelectedItem

        FileTreeItem _SelectedItem;

        public FileTreeItem SelectedItem
        {
            get { return _SelectedItem; }
            set { SetProperty(ref _SelectedItem, value); }
        }

        #endregion

        public MainViewModel()
        {
            ExitCommand = new Command(() => Application.Current.Shutdown());
            SelectItemCommand = new Command(SelectItem);
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                Root = new DirItem();
            else
            {
                WinConsole.Initialize();
                Root = FileTreeLoader.Instance.Load(@"U:\Data");
            }
        }

        void SelectItem(object obj)
        {
            SelectedItem = obj as FileTreeItem;
        }
    }
}
