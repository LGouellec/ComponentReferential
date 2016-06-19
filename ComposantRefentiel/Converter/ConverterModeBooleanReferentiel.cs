using System;
using System.Globalization;
using System.Windows.Data;

namespace ComposantReferentiel.Converter
{
    /// <summary>
    /// Converter Mode Boolean pour déterminer le mode d'affichage de l'écran référentiel.
    /// </summary>
    public class ConverterModeBooleanReferentiel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;

            if (value != null)
            {
                if (System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.CONSULT) || System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.ERASE) || System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.LOADED))
                    result = true;
                if (System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.ADD) || System.Convert.ToInt32(value) == System.Convert.ToInt32(MODE_COMPOSANT_REFERENTIIEL.LOADING))
                    result = false;
            }

            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
