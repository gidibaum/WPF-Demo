using System;
using System.Windows.Data;
using System.Windows.Media;


namespace Base.WPF
{
    public class ColorToBrushConverter : IValueConverter
    {
        #region IValueConverter Members


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (null == value)
                {
                    return null;
                }

                if (value is Color)
                {
                    Color color = (Color)value;
                    return new SolidColorBrush(color);
                }

                if (value is string)
                {
                    BrushConverter conv = new BrushConverter();
                    return conv.ConvertFromString(value as string);
                }
            }
            catch { }

            throw new InvalidOperationException("Unsupported type [" + value.GetType().Name + "]");
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // If necessary, here you can convert back. Check if which brush it is (if its one),
            // get its Color-value and return it.

            throw new NotImplementedException();
        }

        #endregion
    }
}
