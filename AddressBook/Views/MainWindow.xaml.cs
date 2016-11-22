using AddressBook.ViewModels;


namespace AddressBook.Views
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainViewModel();
            DataContext = vm;
            Closing += (sender, args) => vm.Dispose();
        }
    }
}
