using System;
using System.Windows.Data;

namespace ComposantReferentiel.Converter
{
    /// <summary>
    /// Converter permettant de passer de 'O' ou 'N' à true ou false et inversement.
    /// Utilisé dans le binding des checkbox.
    /// A savoir, que si value est un boolean, il est retourné directement sans faire de conversion.
    /// </summary>
    public class ConverterBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Boolean b = System.Convert.ToString(value).ToUpper().Equals("O") ||
            //    System.Convert.ToString(value).Equals("1") ? true : false;
            Boolean b = System.Convert.ToString(value).ToUpper().Equals("O") ||
                System.Convert.ToString(value).ToUpper().Equals("TRUE") ? true : false;

            return b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? (System.Convert.ToBoolean(value) ? "O" : "N") : "N";
        }
    }
}
