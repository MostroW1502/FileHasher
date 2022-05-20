using System;
using System.Globalization;
using System.Windows.Data;

namespace FileHasher
{
    class UIntToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = string.Empty;
            if (value is uint)
            {
                s += $"{value:X8}";
            }
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
