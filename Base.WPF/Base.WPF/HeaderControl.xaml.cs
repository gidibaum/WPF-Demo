using System.Windows;
using System.Windows.Markup;

namespace Base.WPF
{
    [ContentProperty("Item")]

    public partial class HeaderControl
    {
        #region DependencyProperty: Header

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(HeaderControl));

        #endregion

        #region DependencyProperty: HeaderStyle

        public object HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }

        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(HeaderControl));

        #endregion

        #region DependencyProperty: HeaderWidth

        public GridLength HeaderWidth
        {
            get { return (GridLength)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth", typeof(GridLength), typeof(HeaderControl),
            new PropertyMetadata(new GridLength(100)));

        #endregion

        #region Property: Item

        public FrameworkElement Item { get; set; }

        #endregion

        public HeaderControl()
        {
            InitializeComponent();
        }
    }
}
