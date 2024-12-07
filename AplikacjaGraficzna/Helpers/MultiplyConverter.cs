using System;
using System.Globalization;
using System.Windows.Data;

namespace AplikacjaGraficzna.Helpers
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue && parameter is string stringParameter &&
                double.TryParse(stringParameter, NumberStyles.Float, CultureInfo.InvariantCulture, out var factor))
            {
                return doubleValue * factor;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("MultiplyConverter does not support ConvertBack.");
        }
    }
}