using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComposantRefentiel.Converter
{
    public class ConverterDecimal : IValueConverter
    {
        #region IValueConverter Membres

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value != null && value is String)
            {
                double d;
                if (Double.TryParse((String)value, out d))
                    return d;
                else
                    return 0d;
            }
            return 0d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Double)
                return (Double)value;
            else
                return 0d;
        }

        #endregion
    }
}
