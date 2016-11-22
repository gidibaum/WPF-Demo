
using System;

namespace AddressBook.Views
{
    public partial class AddressGridControl
    {
        public event EventHandler DataGridMouseDoubleClick; 

        public AddressGridControl()
        {
            InitializeComponent();
        }

        void dataGrid_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dataGrid.CurrentItem != null)
                DataGridMouseDoubleClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
