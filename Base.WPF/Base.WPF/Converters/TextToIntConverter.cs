using System;
using System.Windows.Data;

namespace Base.WPF
{
    class TextToIntConverter : IValueConverter
    {
        #region IValueConverter Members


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "";
            return value.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            string text = (string)value;
            if (string.IsNullOrEmpty(text))
                return null;

            int val = 0;
            if (int.TryParse(text, out val))
                return (int?)val;
            return null;
        }

        #endregion
    }
}
