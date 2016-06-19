using System;
using System.Globalization;
using System.Windows.Data;

namespace ComposantReferentiel.Converter
{
    /// <summary>
    /// Converteur de date
    /// </summary>
    public class ConverterDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result;
            if (!(value is DateTime))
            {
                result = string.Empty;
            }
            else
            {
                result = System.Convert.ToDateTime(value).ToString();
            }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = System.Convert.ToDateTime(value);
            return dateTime;
        }
    }
}
