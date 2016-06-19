using System;
using System.Globalization;
using System.Windows.Data;

namespace ComposantReferentiel.Converter
{
    public class ConverterModeTitleReferentiel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result;
            if (value != null)
            {
                if (System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.CONSULT))
                {
                    result = "Ajouter";
                    return result;
                }
                if (System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.ADD))
                {
                    result = "Valider";
                    return result;
                }
            }
            result = string.Empty;
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
