using System;
using System.Globalization;
using System.Windows.Data;

namespace FileHasher
{
    class ByteArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] v) { return BytesToString(v); }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string BytesToString(byte[] value)
        {
            string s = string.Empty;
            foreach (byte b in value)
            {
                s += $"{b:X2}";
            }
            return s;
        }
    }
}
