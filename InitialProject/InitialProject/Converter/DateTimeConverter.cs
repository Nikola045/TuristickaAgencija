using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TravelAgency.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime d = (DateTime)value;
            return string.Format("{0}", d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            DateTime dateTimeValue;

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return null;
            }
            else
            {
                if (DateTime.TryParse(stringValue, out dateTimeValue))
                {
                    return dateTimeValue;
                }
                else
                {
                    throw new ArgumentException("Invalid date time value");
                }
            }
        }
    }
}

