using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace cm.frontend.core.Domain.Utilities.Converters
{
    public class ToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = (DateTimeOffset) value;
                return date.DateTime;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return DateTime.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = (DateTime) value;
                return new DateTimeOffset(date);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return DateTimeOffset.MinValue;
            }
        }

    }
}
