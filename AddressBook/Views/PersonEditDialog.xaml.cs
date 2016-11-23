using System.Windows;
using System.Windows.Input;

namespace AddressBook.Views
{
    public partial class PersonEditDialog
    {

        public PersonEditDialog()
        {
            InitializeComponent();

            this.PreviewKeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    Close();
                }
            };
        }

        void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
