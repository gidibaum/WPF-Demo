using System.Windows;

namespace AddressBook.Views
{
    public partial class PersonEditDialog
    {
        public PersonEditDialog()
        {
            InitializeComponent();
        }

        void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
