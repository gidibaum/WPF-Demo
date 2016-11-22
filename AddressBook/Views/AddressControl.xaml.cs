
using System.Windows;

namespace AddressBook.Views
{ 
    public partial class AddressControl
    {
        #region DependencyProperty: IsReadOnly

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(AddressControl), new PropertyMetadata(false));

        #endregion

        public AddressControl()
        {
            InitializeComponent();
        }
    }
}
